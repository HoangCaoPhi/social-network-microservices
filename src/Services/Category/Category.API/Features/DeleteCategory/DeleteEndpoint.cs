using Category.API.Infrastructure.Context;
using Category.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Category.API.Features.DeleteCategory;

internal sealed class DeleteEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", async (Ulid id, CategoryWriteDbContext context) =>
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
                return Result<bool>.Failure(CategoryErrors.NotFound);
 
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Result<bool>.Success(true);
        })
       .Produces<Result<bool>>(StatusCodes.Status200OK)
       .Produces<ProblemDetails>(StatusCodes.Status400BadRequest); 
    }
}
