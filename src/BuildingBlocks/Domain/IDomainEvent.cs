using MediatR;

namespace Domain;
public interface IDomainEvent : INotification
{
    public Ulid Id { get; init; }
}
