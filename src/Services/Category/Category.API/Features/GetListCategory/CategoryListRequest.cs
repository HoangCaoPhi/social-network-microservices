using Application.Models;

namespace Category.API.Features.GetListCategory;

public class CategoryListRequest : PagingSortingModel
{
    public string? Name { get; set; }

    public string? CategoryStatus { get; set; }
}
