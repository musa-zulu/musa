using B2B.Application.Common.Interfaces;
using B2B.Domain.Entities;
using MediatR;

namespace B2B.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository repo, ICacheService cache) : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repo = repo;
    private readonly ICacheService _cache = cache;

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            SKU = request.SKU,
            UnitPrice = request.UnitPrice,
            Category = request.Category,
            AvailableQuantity = request.AvailableQuantity
        };

        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();

        await _cache.RemoveAsync("products");

        return product.Id;
    }
}