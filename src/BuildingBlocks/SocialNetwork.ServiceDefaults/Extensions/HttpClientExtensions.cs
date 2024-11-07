using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SocialNetwork.ServiceDefaults.Extensions;
public static class HttpClientExtensions
{
    public static IHostApplicationBuilder AddAuthToken(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor(); 
        return builder;
    }
}
