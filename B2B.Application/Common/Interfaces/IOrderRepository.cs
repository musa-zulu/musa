using B2B.Domain.Entities;

namespace B2B.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
}
