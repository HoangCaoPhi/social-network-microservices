using FluentValidation;

namespace Category.API.Features.UpdateCategory;


public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
{
    public CategoryUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(255);
    }
}
