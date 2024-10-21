namespace Category.API.Features.CreateCategory;

internal sealed class CreateEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async () =>
        {

        });
    }
}
