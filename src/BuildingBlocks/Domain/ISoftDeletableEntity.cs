namespace Domain;
public interface ISoftDeletableEntity
{
    DateTime? DeletedAt { get; }

    bool IsDeleted { get; }
}
