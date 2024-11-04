using Category.API.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace Category.API.Features.CreateCategory;

internal sealed class CreateEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] CreateCategoryRequest request,
                                CategoryDbContext context) =>
        {
            var category = Models.Category.Create(request.Name, request.Description);
            context.Add(category);
            await context.SaveChangesAsync();
            return category.Id;
        })
       .Produces<Ulid>(StatusCodes.Status200OK)
       .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}
