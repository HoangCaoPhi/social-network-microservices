﻿using Category.API.Infrastructure.Context;
using FluentValidation;
using Shared.Constants;
using SocialNetwork.ServiceDefaults.Extensions;
namespace Category.API.Extensions;

public static class Extentions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        builder.AddDefaultAuthentication();

        builder.AddWriteDbContext<CategoryWriteDbContext>(options =>
        {
            options.ConnectionStringSection = ConnectionStringSection.CategoryDb;
        });

        builder.AddReadDbContext<CategoryReadDbContext>(options =>
        {
            options.ConnectionStringSection = ConnectionStringSection.CategoryDb;            
        });

        services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}
