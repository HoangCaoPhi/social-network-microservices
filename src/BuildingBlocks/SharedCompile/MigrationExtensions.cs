using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Hosting;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TContext>(this WebApplication app)
    where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
 
        dbContext.Database.Migrate();
    }
}