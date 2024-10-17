namespace Post.Domain.Seedwork;

internal interface ISoftDeletableEntity
{
    DateTime? DeletedAt { get; }

    bool IsDeleted { get; }
}
