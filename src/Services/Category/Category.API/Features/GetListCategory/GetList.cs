using Application.Extensions;
using Category.API.Infrastructure.Context;
using Mapster;

namespace Category.API.Features.GetListCategory;

internal sealed class GetList : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("", async ([AsParameters] CategoryListRequest request, 
                              CategoryReadDbContext context) =>
        {
            var categorySpecific = new CategoryListSpecification(request);
            var result = await context
                                .ApplySpecification(categorySpecific)
                                .ProjectToType<CategoryListViewModel>()
                                .PagingAsync(request);
            return result;
        });
    }
}
