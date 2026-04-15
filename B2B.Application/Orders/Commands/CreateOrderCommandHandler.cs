using B2B.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace B2B.Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IUnitOfWork _uow,
    IMessageQueue _queue,
    IOrderService _orderService,
    ICacheService _cacheService,
    IOrderRepository _orderRepository,
    IProductRepository _productRepository,
    IIdempotencyService _idempotencyService)
     : IRequestHandler<CreateOrderCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.IdempotencyKey))
        {
            var existing = await _idempotencyService.GetAsync(request.IdempotencyKey, request.CustomerRef);

            if (existing.HasValue)
            {
                return existing.Value;
            }

            Guid result = Guid.Empty;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    await _uow.BeginTransactionAsync();

                    var ids = request.OrderItems.Select(x => x.ProductId).ToList();
                    var products = await _productRepository.GetByIdsAsync(ids);

                    var mapped = request
                        .OrderItems
                        .Select(i => (products.FirstOrDefault(p => p.Id == i.ProductId), i.Quantity))
                        .ToList();

                    var order = _orderService.Create(request.CustomerRef, mapped);

                    await _orderRepository.AddAsync(order);

                    await _uow.SaveChangesAsync();
                    await _uow.CommitTransactionAsync();

                    result = order.Id;
                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await _uow.RollbackAsync();
                    await Task.Delay(100);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.IdempotencyKey))
                await _idempotencyService.StoreAsync(request.IdempotencyKey, request.CustomerRef, result);

            await _cacheService.RemoveAsync("products");

            await _queue.PublishAsync(new { OrderId = result });

            return result;
        }
    }
}
