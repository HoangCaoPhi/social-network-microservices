namespace Category.API.Models;

public sealed class Category
{
    public Ulid Id { get; init; }

    public required string Name { get; init; }

    public string? Description { get; init; }    

    public CategoryStatus Status { get; init; }
}
