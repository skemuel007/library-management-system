using CQRS_Test.CQRS.Queries;
using CQRS_Test.Data;
using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Handlers;

public class GetsProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly FakeDataStore _fakeDataStore;

    public GetsProductsHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _fakeDataStore.GetAllProducts();
    }
}