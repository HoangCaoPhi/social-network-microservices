using Category.API.Infrastructure.Context;
using Category.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Category.API.Features.ActiveCategory;

internal sealed class ActiveEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("{categoryId}/active", async (Guid categoryId, CategoryWriteDbContext context) =>
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

            if (category is null)
                return Result<bool>.Failure(CategoryErrors.NotFound);
 
            var result = category.ActiveCategory();
            if (!result.IsSuccess)
                return Result<bool>.Failure(result.Error!);

            await context.SaveChangesAsync();
            return Result<bool>.Success(true);
        })
       .Produces<Result<bool>>(StatusCodes.Status200OK)
       .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}
