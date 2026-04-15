using B2B.Application.Common.Interfaces;
using B2B.Domain.Entities;
using B2B.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2B.Infrastructure.Orders.Persistence;

public class OrderRepository(AppDbContext _dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
        => await _dbContext.Orders.AddAsync(order);

    public async Task<Order?> GetByIdAsync(Guid id) 
        => await _dbContext.Orders.Include(o => o.OrderItems)
                           .FirstOrDefaultAsync();

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}
