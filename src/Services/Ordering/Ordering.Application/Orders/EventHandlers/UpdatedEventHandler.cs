namespace Ordering.Application.Orders.EventHandlers;

public class UpdatedEventHandler(ILogger<CreatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public async Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        throw new NotImplementedException();
    }
}