using CQRS_Test.CQRS.Notifications;
using CQRS_Test.Data;
using MediatR;

namespace CQRS_Test.CQRS.Handlers;

public class CacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
{
    private readonly FakeDataStore _fakeDataStore;
    public CacheInvalidationHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
    
    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _fakeDataStore.EventOccured(notification.Product, "Cache Invalidated");
        await Task.CompletedTask;
    }
}