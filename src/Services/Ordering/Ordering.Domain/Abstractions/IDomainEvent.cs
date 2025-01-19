using MediatR;

namespace Ordering.Domain.Abstractions;
public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    // TODO: create ITimeProvider and use that instead
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
