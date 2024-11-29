using Microsoft.EntityFrameworkCore;

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
}