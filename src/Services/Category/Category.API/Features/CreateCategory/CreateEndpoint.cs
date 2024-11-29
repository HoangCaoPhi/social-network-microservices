using Category.API.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Services;

namespace Category.API.Features.CreateCategory;

internal sealed class CreateEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async ([FromBody] CreateCategoryRequest request,
                                CategoryWriteDbContext context,
                                IGuidGenerator guidGenerator) =>
        {
            var category = Models.Category.Create(guidGenerator.NewGuid(),
                                                  request.Name, 
                                                  request.Description);

            context.Add(category);
            await context.SaveChangesAsync();

            return Result<Guid>.Success(category.Id);
        })
       .Produces<Result<Guid>>(StatusCodes.Status200OK)
       .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);
    }
}
