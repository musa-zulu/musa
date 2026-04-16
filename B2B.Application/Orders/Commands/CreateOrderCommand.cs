using B2B.Domain.Entities;
using ErrorOr;
using MediatR;

namespace B2B.Application.Orders.Commands;

public record CreateOrderCommand(
    List<OrderItem> OrderItems,
    string? IdempotencyKey
    ) : IRequest<Guid>
{
    public string CustomerRef { get; set; } = string.Empty;
}