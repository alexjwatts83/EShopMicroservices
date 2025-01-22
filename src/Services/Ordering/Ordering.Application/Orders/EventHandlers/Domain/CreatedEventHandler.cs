using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class CreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<CreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        // TODO have a key instead of a magic string
        var orderFullfilmentEnabled = await featureManager.IsEnabledAsync("OrderFullfilment");
        if (!orderFullfilmentEnabled)
        {
            logger.LogInformation("OrderFullfilment Not enabled");
            return;
        }

        var orderCreatedIntegrationEvent = domainEvent.Order.ToDto();

        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}
