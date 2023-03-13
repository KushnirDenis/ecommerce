using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class CategoryNameDto
{
    public string Name { get; set; }
    public Language Language { get; set; }

    public CategoryName ToCategoryName() => new(Name, Language);
    public CategoryName ToCategoryName(int categoryId) => new(categoryId, Name, Language);
}

public class CategoryNameDtoValidator : AbstractValidator<CategoryNameDto>
{
    public CategoryNameDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(localizer["InvalidValue"]);
        RuleFor(c => c.Language).IsInEnum()
            .WithMessage(localizer["InvalidValue"]);
    }
}