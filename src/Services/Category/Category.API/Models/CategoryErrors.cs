using Shared;

namespace Category.API.Models;

public static class CategoryErrors
{
    public static readonly Error NotFound = Error.NotFound(
       "Category.NotFound",
       $"The category not found!");

    public static readonly Error AlreadyActive = Error.NotFound(
        "Category.AlreadyActive",
        "The category was already activated!"
    );

    public static readonly Error AlreadyInactive = Error.NotFound(
        "Category.AlreadyInactive",
        "The category has already been deactivated!"
    );
}
