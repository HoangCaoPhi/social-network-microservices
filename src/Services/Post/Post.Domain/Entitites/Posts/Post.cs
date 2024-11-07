using Domain;
using Shared;

namespace Post.Domain.Entitites.Posts;

public class Post : AggregateRoot, IAuditableEntity
{
    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string Content { get; private set; }

    public string CategoryId { get; private set; }

    public string CategoryName { get; private set; }

    public Author Author { get; private set; }

    public Attachment Attachment { get; private set; }

    public PostType PostType { get; private set; }

    public PostPrivacy PostPrivacy { get; private set; }

    public IEnumerable<Tag> Tags { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public string CreateBy { get; private set; }

    public DateTime? ModifiedAt { get; private set; }

    public string? CreatedBy { get; private set; }

    public string? ModifiedBy { get; private set; }
}