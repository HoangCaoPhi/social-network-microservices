using Microsoft.EntityFrameworkCore;
using Persistence.Abtractions;

namespace Category.API.Infrastructure.Context;

public class CategoryWriteDbContext(DbContextOptions<CategoryWriteDbContext> options,
                               IConfiguration configuration) : DbContext(options)
{
    public DbSet<Models.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IAssemblyMarker).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();
    }
}