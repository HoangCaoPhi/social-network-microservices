namespace Category.API.Features.DeactiveCategory;

public sealed class DeactiveEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("{id}/deactive", async () =>
        {

        });
    }
}
