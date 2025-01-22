using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class CreatedEventHandler(IPublishEndpoint publishEndpoint, ILogger<CreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        var orderCreatedIntegrationEvent = domainEvent.Order.ToDto();

        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
