using Domain;

namespace Post.Domain.Events;
public record PostCreatedEvent(Ulid Id) : IDomainEvent;