using B2B.Application.Common.Interfaces;
using B2B.Domain.Entities;
using B2B.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2B.Infrastructure.Products.Persistence;

public class ProductRepository(AppDbContext _dbContext) : IProductRepository
{
    public async Task AddAsync(Product product) =>
        await _dbContext.Products.AddAsync(product);

    public async Task<List<Product>> GetAllAsync() =>
        await _dbContext.Products.AsNoTracking().ToListAsync();

    public async Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        => await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();

    public async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();
}
