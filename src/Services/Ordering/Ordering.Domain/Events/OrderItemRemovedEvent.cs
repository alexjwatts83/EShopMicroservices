namespace Ordering.Domain.Events;

public record OrderItemRemovedEvent(Order Order, OrderItem Item) : IDomainEvent;
