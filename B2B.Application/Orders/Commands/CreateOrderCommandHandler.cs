using B2B.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using System;

namespace B2B.Application.Orders.Commands;

public class CreateOrderCommandHandler(
    IUnitOfWork _uow,    
    IOrderService _orderService,
    ICacheService _cacheService,
    IOutboxService _outboxService,
    IOrderRepository _orderRepository,
    IProductRepository _productRepository,
    IIdempotencyService _idempotencyService)
     : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<DbUpdateConcurrencyException>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(100));

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.IdempotencyKey))
        {
            return Guid.Empty;
        }
        var existing = await _idempotencyService.GetAsync(request.IdempotencyKey, request.CustomerRef);

        if (existing.HasValue)
        {
            return existing.Value;
        }

        Guid result = Guid.Empty;

        await _retryPolicy.ExecuteAsync(async () =>
        {
            await _uow.BeginTransactionAsync();

            var ids = request.OrderItems.Select(x => x.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(ids);

            var mapped = request
                .OrderItems
                .Select(i => (products.First(p => p.Id == i.ProductId), i.Quantity))
                .ToList();

            var order = _orderService.Create(request.CustomerRef, mapped);

            await _orderRepository.AddAsync(order);

            await _uow.SaveChangesAsync(cancellationToken);
            await _uow.CommitTransactionAsync(cancellationToken);

            result = order.Id;
        });

        if (!string.IsNullOrWhiteSpace(request.IdempotencyKey))
            await _idempotencyService.StoreAsync(request.IdempotencyKey, request.CustomerRef, result);

        await _cacheService.RemoveAsync("products");

        await _outboxService.AddAsync("OrderCreated", System.Text.Json.JsonSerializer.Serialize(new { OrderId = result }));

        return result;
    }
}
