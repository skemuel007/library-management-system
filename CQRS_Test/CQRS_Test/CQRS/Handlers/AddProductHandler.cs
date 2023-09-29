using System.Runtime.InteropServices;
using CQRS_Test.CQRS.Commands;
using CQRS_Test.Data;
using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Handlers;

public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly FakeDataStore _fakeDataStore;

    public AddProductHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
    
    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _fakeDataStore.AddProduct(request.Product);
        return request.Product;
    }
}