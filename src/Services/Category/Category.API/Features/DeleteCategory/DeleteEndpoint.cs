namespace Category.API.Features.DeleteCategory;

internal sealed class DeleteEndpoint : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id}", async () =>
        {

        });
    }
}
