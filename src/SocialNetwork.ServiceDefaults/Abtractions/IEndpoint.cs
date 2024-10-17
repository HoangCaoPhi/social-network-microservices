using Microsoft.AspNetCore.Routing;

namespace SocialNetwork.ServiceDefaults.Abtractions;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}