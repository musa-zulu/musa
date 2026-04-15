namespace B2B.Contracts.Orders;

public record CreateOrderRequest(
    List<OrderItemDto> OrderItems);
