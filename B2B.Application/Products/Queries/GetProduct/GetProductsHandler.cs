using B2B.Application.Common.Interfaces;
using B2B.Domain.Entities;
using ErrorOr;
using MediatR;

namespace B2B.Application.Products.Queries.GetProduct;

public class GetProductsHandler(
    IProductRepository _productRepository,
    ICacheService _cacheService)
    : IRequestHandler<GetProducutsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(
        GetProducutsQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = "products";

        var cached = await _cacheService.GetAsync<List<Product>>(cacheKey);
        if (cached is not null) return cached;

        var products = await _productRepository.GetAllAsync();

        await _cacheService.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));

        return products;
    }
}
