using Category.API.Infrastructure.Context;
using Shared.Constants;
using SocialNetwork.ServiceDefaults.Extensions;

namespace Category.API.Extensions;

public static class Extentions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        builder.AddDefaultAuthentication();

        builder.AddSqlServerService<CategoryDbContext>(options =>
        {
            options.ConnectionStringSection = ConnectionStringSection.CategoryDb;
        });
    }
}
