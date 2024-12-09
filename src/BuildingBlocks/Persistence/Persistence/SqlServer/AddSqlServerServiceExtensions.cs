using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.SqlServer;
public static class AddSqlServerServiceExtensions
{
    public static void AddWriteDbContext<TContext>(this IHostApplicationBuilder builder, Action<SqlServerOptions> options)
        where TContext : DbContext
    {
        var sqlServerOptions = new SqlServerOptions();
        options(sqlServerOptions);

        builder.Services.AddSingleton<UpdateAuditableEntityInterceptor>();
 
        builder.AddSqlServerDbContext<TContext>(
            connectionName: sqlServerOptions.ConnectionStringSection,
            configureDbContextOptions: options =>
            {                
                options.AddInterceptors(builder.Services.BuildServiceProvider().GetService<UpdateAuditableEntityInterceptor>()!);
            });
    }
}
