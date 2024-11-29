using Domain;

namespace Post.Domain.Events;
public record PostCreatedEvent(Guid Id) : IDomainEvent;