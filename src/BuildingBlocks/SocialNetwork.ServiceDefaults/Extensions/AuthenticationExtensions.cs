using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Constants;

namespace SocialNetwork.ServiceDefaults.Extensions;
public static class AuthenticationExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
 
        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }

        var audience = identitySection.GetRequiredValue("Audience");

        services
            .AddAuthentication()
            .AddKeycloakJwtBearer(ServiceName.Keycloak,
                                 nameof(SocialNetwork),
                                 options =>
           {
                  options.Audience = audience;
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters.ValidateAudience = false;
           });

        services.AddAuthorization();

        return services;
    }
}
