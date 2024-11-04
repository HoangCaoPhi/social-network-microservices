using Category.API.Infrastructure.Context;
using Category.API.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Category.API.Features.GetOneCategory;

public sealed class GetOneEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("{categoryId}", async (Ulid categoryId, CategoryReadDbContext context) => {

            var category = await context
                                    .GetCategories()
                                    .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category is null)
                return Result<CategoryReadModel>.Failure(CategoryErrors.NotFound);
 
            return Result<CategoryReadModel>.Success(category);
        });
    }
}
