using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Persistence.SqlServer;
public static class AddSqlServerServiceExtensions
{
    public static void AddSqlServerService<TContext>(this IHostApplicationBuilder builder, Action<SqlServerOptions> options)
        where TContext : DbContext
    {
        var sqlServerOptions = new SqlServerOptions();
        options(sqlServerOptions);

        builder.AddSqlServerDbContext<TContext>(sqlServerOptions.ConnectionStringSection);
    }
}
