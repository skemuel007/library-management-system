using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Queries;

public record GetProductsQuery() : IRequest<IEnumerable<Product>>;