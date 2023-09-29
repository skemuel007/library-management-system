using CQRS_Test.CQRS.Notifications;
using CQRS_Test.Data;
using MediatR;

namespace CQRS_Test.CQRS.Handlers;

public class EmailHandler : INotificationHandler<ProductAddedNotification>
{
    private readonly FakeDataStore _fakeDataStore;
    public EmailHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
    
    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _fakeDataStore.EventOccured(notification.Product, "Email sent");
        await Task.CompletedTask;
    }
}