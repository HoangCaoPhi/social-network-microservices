using Domain;
using Shared;

namespace Category.API.Models;

public sealed class Category : Entity
{
    public string Name { get; private set; }

    public string? Description { get; private set; }    

    public CategoryStatus Status { get; private set; }

    public static Category Create(string name, string? description)
    {
        return new Category()
        {
            Id = Ulid.NewUlid(),
            Name = name,
            Description = description,
            Status = CategoryStatus.Inactive,
        };
    }

    public void Update(string name, string? description, CategoryStatus status)
    {
        Name = name;
        Description = description;
        Status = status;
    }

    public Result ActiveCategory()
    {
        if(Status is CategoryStatus.Active)
            return CategoryErrors.AlreadyActive;

        Status = CategoryStatus.Active;

        return Result.Success();
    }

    public Result DeactiveCategory()
    {
        if (Status is CategoryStatus.Inactive)
            return CategoryErrors.AlreadyInactive;

        Status = CategoryStatus.Inactive;

        return Result.Success();
    }
}
