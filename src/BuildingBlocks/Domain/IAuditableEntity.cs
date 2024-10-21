namespace Domain;
public interface IAuditableEntity
{
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; }
}