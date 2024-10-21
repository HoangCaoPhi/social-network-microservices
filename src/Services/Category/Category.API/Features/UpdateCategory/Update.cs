namespace Category.API.Features.UpdateCategory;

public sealed class Update : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("{id}", async () =>
        {

        });
    }
}
