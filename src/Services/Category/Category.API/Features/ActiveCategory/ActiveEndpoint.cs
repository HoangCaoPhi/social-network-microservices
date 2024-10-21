namespace Category.API.Features.ActiveCategory;

internal sealed class ActiveEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("{id}/active", async () =>
        {

        });
    }
}
