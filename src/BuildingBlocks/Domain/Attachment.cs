
namespace Domain;
public class Attachment : ValueObject
{    
    public string Name { get; private set; }

    public string DisplayName { get; private set; }

    public string Extension { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return new object[] { Name, DisplayName, Extension };
    }
}
