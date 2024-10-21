namespace Category.API.Features.GetOneCategory;

public sealed class GetOneEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("{id}", async () => { });
    }
}
