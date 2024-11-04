using Application.Models;
using Ardalis.Specification;

namespace Application.Abtractions;
public interface IPagingSpecification<T, TFilter> : ISpecification<T> 
    where TFilter : PagingSortingModel
{
    ISpecificationBuilder<T> ApplyOrdering(ISpecificationBuilder<T> builder, TFilter? filter = null);
}
