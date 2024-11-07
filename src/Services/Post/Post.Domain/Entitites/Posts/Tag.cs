using Domain;

namespace Post.Domain.Entitites.Posts;
public class Tag : ValueObject
{
    public string TagName { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return TagName;
    }
}