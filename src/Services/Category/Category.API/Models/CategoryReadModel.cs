namespace Category.API.Models;

public class CategoryReadModel
{
    public Ulid Id { get; set; }
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public string Status { get; private set; }
}
