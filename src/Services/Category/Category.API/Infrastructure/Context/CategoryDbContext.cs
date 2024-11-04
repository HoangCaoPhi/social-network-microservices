using Microsoft.EntityFrameworkCore;
using Persistence.Abtractions;

namespace Category.API.Infrastructure.Context;

public class CategoryDbContext(DbContextOptions<CategoryDbContext> options,
                               IConfiguration configuration) : DbContext(options)
{
    public DbSet<Models.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();
    }
}