using CQRS_Test.CQRS.Queries;
using CQRS_Test.Data;
using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly FakeDataStore _fakeDataStore;

    public GetProductByIdHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _fakeDataStore.GetProductById(request.Id);
    }
}