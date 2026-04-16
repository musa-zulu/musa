using B2B.Domain.Entities;

namespace B2B.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task<List<Product>> GetByIdsAsync(List<Guid> ids);
}
