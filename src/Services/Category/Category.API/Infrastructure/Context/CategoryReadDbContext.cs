using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Category.API.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Abtractions;

namespace Category.API.Infrastructure.Context;

public class CategoryReadDbContext(DbContextOptions<CategoryReadDbContext> options) : DbContext(options) 
{
    public DbSet<CategoryReadModel> Categories { get; set; }

    public IQueryable<CategoryReadModel> GetCategories() => Set<CategoryReadModel>().AsQueryable();

    public IQueryable<CategoryReadModel> ApplySpecification(
        ISpecification<CategoryReadModel> specification)
            => SpecificationEvaluator.Default.GetQuery(GetCategories(), specification);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IAssemblyMarker).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidToStringConverter>();
    }
}
