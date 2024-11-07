using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shared;
using Microsoft.Extensions.DependencyInjection;

public sealed class UpdateAuditableEntityInterceptor(IServiceProvider serviceProvider) :
    SaveChangesInterceptor
{ 
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
 
        var entries = dbContext
                        .ChangeTracker
                        .Entries<IAuditableEntity>();

        using var scope = serviceProvider.CreateScope();
        var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.CreatedBy).CurrentValue = identityService.GetUserIdentity();
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedAt).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.ModifiedBy).CurrentValue = identityService.GetUserIdentity();
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
