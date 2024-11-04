using Domain;

namespace Category.API.Models;

public sealed class Category : Entity
{
    public required string Name { get; init; }

    public string? Description { get; init; }    

    public CategoryStatus Status { get; init; }

    public static Category Create(string name, string description)
    {
        return new Category()
        {
            Id = Ulid.NewUlid(),
            Name = name,
            Description = description,
            Status = CategoryStatus.Inactive,
        };
    }
}
