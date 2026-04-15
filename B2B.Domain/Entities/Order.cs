namespace B2B.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerRef { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
}
