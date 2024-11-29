using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Category.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Category.API.Infrastructure.Context;

public class CategoryReadDbContext(CategoryWriteDbContext dbContext)  
{
    public DbSet<CategoryReadModel> Categories { get; set; }

    public IQueryable<CategoryReadModel> GetCategories() => GetQuery<CategoryReadModel>();

    public IQueryable<CategoryReadModel> ApplySpecification(
        ISpecification<CategoryReadModel> specification)
            => SpecificationEvaluator.Default.GetQuery(GetCategories(), specification);
 
    public IQueryable<TEntity> GetQuery<TEntity>()
        where TEntity : class
    {
        return dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();
    }
}
