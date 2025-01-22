namespace Ordering.Application.Orders.EventHandlers.Domain;

public class CreatedEventHandler(ILogger<CreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        //throw new NotImplementedException();
    }
}
