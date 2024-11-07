using Domain;

namespace Post.Domain.Entitites.Posts;
public class Author : ValueObject
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return new object[] { Name, Email };
    }
}
