using B2B.Domain.Entities;

namespace B2B.Application.Common.Interfaces;

public interface IOrderService
{
    Order Create(string customer, List<(Product product, int quantity)> orderItems);
}
