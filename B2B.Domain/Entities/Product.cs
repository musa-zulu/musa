using System.ComponentModel.DataAnnotations;

namespace B2B.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int AvailableQuantity { get; set; }
    public string? Category { get; set; } = string.Empty;

    [Timestamp]
    public byte[] RowVersion { get; set; } = default!;
}
