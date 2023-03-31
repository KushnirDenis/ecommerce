using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class ProductDescriptionDto
{
    public string Description { get; set; }
    public Language Language { get; set; }

    public ProductDescription ToProductDescription()
    {
        return new ProductDescription()
        {
            Description = Description,
            Language = Language
        };
    }
}

public class ProductDescriptionDtoValidator : AbstractValidator<ProductDescriptionDto>
{
    public ProductDescriptionDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage(localizer["InvalidValue"]);
        RuleFor(c => c.Language).IsInEnum()
            .WithMessage(localizer["InvalidValue"]);
    }
}