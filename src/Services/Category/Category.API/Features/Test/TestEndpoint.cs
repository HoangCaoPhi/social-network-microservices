namespace Category.API.Features.Test;

public class TestEndpoint : IEndpoint
{
    public string GroupEntityEndpoint()
    {
        return "Test";
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("", () =>
        {
            return "Ok";
        });
    }
}
