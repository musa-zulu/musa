using B2B.Application.Common.Interfaces;
using B2B.Domain.Entities;

namespace B2B.Application.Common.Services;

public class OrderService(IDateTimeProvider _dateTimeProvider)
{
    public Order Create(string customer, List<(Product product, int quantity)> orderItems)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerRef = customer,
            CreatedDate = _dateTimeProvider.UtcNow,
        };

        foreach (var (product, quantity) in orderItems)
        {
            if (product.AvailableQuantity < quantity)
                throw new Exception("Insufficient stock"); //TODO: clean up exceptions to throw specific exception

            product.AvailableQuantity -= quantity;

            order.OrderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = quantity,
                UnitPrice = product.UnitPrice
            });
        }

        return order;
    }
}
