using Application.Abtractions;
using Application.Models;
using Ardalis.Specification;
using Category.API.Models;

namespace Category.API.Features.GetListCategory
{
    public class CategoryListSpecification : Specification<CategoryReadModel>, 
                        IPagingSpecification<CategoryReadModel, CategoryListRequest>
    {
        public CategoryListSpecification(CategoryListRequest filter)
        {
            Query.Where(c => c.Name == filter.Name, filter.Name is not null)
                 .Where(c => c.Status == filter.CategoryStatus, filter.CategoryStatus is not null);
                 
            ApplyOrdering(Query, filter);
        }

        public ISpecificationBuilder<CategoryReadModel> ApplyOrdering(
            ISpecificationBuilder<CategoryReadModel> builder, 
            CategoryListRequest? filter = null)
        {
            if (filter is null) return builder.OrderBy(x => x.Id);

            var isAscending = filter.OrderDirection == OrderDirectionEnum.ASC.ToString();

            return filter.OrderField switch
            {
                nameof(CategoryReadModel.Name) => isAscending ? builder.OrderBy(x => x.Name) : builder.OrderByDescending(x => x.Name),
                _ => builder.OrderBy(x => x.Id)
            };
        }
    }
}
