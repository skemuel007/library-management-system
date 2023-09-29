using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Commands;

public record AddProductCommand(Product Product): IRequest<Product>;