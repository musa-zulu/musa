namespace B2B.Contracts.Orders;

public record OrderItemDto(
    Guid ProductId,
    int Quantity);