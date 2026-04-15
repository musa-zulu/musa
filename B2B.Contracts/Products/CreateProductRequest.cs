namespace B2B.Contracts.Products;

public record CreateProductRequest(
    string Name,
    string SKU,
    decimal UnitPrice,
    int AvailableQuantity,
    string? Category);