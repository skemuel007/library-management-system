using CQRS_Test.Models;
using MediatR;

namespace CQRS_Test.CQRS.Notifications;

public record ProductAddedNotification(Product Product): INotification;