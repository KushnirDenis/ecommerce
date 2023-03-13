using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class AddNameInAnotherLanguageDto
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Language Language { get; set; }
}

public class AddNameInAnotherLanguageDtoValidator : AbstractValidator<AddNameInAnotherLanguageDto>
{
    public AddNameInAnotherLanguageDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(c => c.Name)
            .Must(name => !string.IsNullOrWhiteSpace(name))
            .WithMessage(localizer["InvalidValue"]);
        RuleFor(c => c.Language).IsInEnum()
            .WithMessage(localizer["InvalidValue"]);
    }
}

