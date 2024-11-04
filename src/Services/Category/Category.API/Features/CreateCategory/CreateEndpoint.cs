using Category.API.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Category.API.Features.CreateCategory;

internal sealed class CreateEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] CreateCategoryRequest request,
                                CategoryWriteDbContext context) =>
        {
            var category = Models.Category.Create(request.Name, request.Description);

            context.Add(category);
            await context.SaveChangesAsync();

            return Result<Ulid>.Success(category.Id);
        })
       .Produces<Result<Ulid>>(StatusCodes.Status200OK)
       .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}
