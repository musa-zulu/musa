using MediatR;

namespace B2B.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string SKU,
    decimal UnitPrice,
    int AvailableQuantity,
    string? Category) : IRequest<Guid>;
