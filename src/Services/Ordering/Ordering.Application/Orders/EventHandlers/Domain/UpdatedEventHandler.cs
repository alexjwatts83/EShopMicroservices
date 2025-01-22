namespace Ordering.Application.Orders.EventHandlers.Domain;

public class UpdatedEventHandler(ILogger<UpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public async Task Handle(OrderUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        //throw new NotImplementedException();
    }
}