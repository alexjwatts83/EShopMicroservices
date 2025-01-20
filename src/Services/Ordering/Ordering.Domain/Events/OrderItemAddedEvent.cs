namespace Ordering.Domain.Events;

public record OrderItemAddedEvent(Order Order, OrderItem Item) : IDomainEvent;
