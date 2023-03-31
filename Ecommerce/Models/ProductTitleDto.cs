using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class ProductTitleDto
{
    public string Title { get; set; }
    public Language Language { get; set; }

    public ProductTitle ToProductTitle()
    {
        return new ProductTitle()
        {
            Title = Title,
            Language = Language
        };
    }
}

public class ProductTitleDtoValidator : AbstractValidator<ProductTitleDto>
{
    public ProductTitleDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage(localizer["InvalidValue"]);
        RuleFor(c => c.Language).IsInEnum()
            .WithMessage(localizer["InvalidValue"]);
    }
}