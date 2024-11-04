using Category.API.Models;

namespace Category.API.Features.UpdateCategory;

public record CategoryUpdateRequest(
    string Name,
    string Description,
    CategoryStatus Status);