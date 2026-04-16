using B2B.Domain.Entities;
using ErrorOr;
using MediatR;

namespace B2B.Application.Products.Queries.GetProduct;

public record GetProducutsQuery() : IRequest<List<Product>>;