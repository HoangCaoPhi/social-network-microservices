namespace Category.API.Features.GetListCategory;

internal sealed class GetList : IEndpoint
{
    public string GroupEntityEndpoint() => Endpoint.Category;
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("", async () =>
        {

        });
    }
}
