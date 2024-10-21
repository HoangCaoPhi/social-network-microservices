using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SocialNetwork.ServiceDefaults.Abtractions;
using System.Reflection;

namespace SocialNetwork.ServiceDefaults.Extensions;

public static class EndpointExtensions
{
    public static void AddMinimalEndpoints(this IServiceCollection services, Assembly assembly)
    {
         var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type.IsClass && !type.IsAbstract && typeof(IEndpoint).IsAssignableFrom(type))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type.AsType()))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }

    public static IApplicationBuilder MapMinimalEndpoints<T>(
       this WebApplication app,
       RouteGroupBuilder? routeGroupBuilder)
       where T : IEndpoint
    {
        IEnumerable<T> endpoints = app.Services.GetRequiredService<IEnumerable<T>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            var groupBuilder = builder
                                .MapGroup(endpoint.GroupEntityEndpoint())
                                .WithTags(endpoint.GroupEntityEndpoint());
            endpoint.MapEndpoint(groupBuilder);
        }

        return app;
    }

    public static IApplicationBuilder MapMinimalEndpoints(
       this WebApplication app,
       RouteGroupBuilder? routeGroupBuilder)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? 
                                    app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            var groupBuilder = builder
                                .MapGroup(endpoint.GroupEntityEndpoint())
                                .WithTags(endpoint.GroupEntityEndpoint());
            endpoint.MapEndpoint(groupBuilder);
        }

        return app;
    }
}