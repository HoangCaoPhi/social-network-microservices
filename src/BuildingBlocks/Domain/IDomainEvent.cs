using MediatR;

namespace Domain;
public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
