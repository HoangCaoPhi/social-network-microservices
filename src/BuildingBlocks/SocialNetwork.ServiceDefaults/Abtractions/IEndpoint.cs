using Microsoft.AspNetCore.Routing;

namespace SocialNetwork.ServiceDefaults.Abtractions;
public interface IEndpoint
{
    string GroupEntityEndpoint();
    void MapEndpoint(IEndpointRouteBuilder app);
}
 