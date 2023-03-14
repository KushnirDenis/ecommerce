using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class CreateCategoryDto
{
    public int? ParentCategoryId { get; set; }
    public List<CategoryNameDto> Names { get; set; }
    /// <summary>
    /// Base64 Image
    /// </summary>
    public Image Image { get; set; }
}

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleForEach(c => c.Names)
            .SetValidator(new CategoryNameDtoValidator(localizer));
        RuleFor(c => c.Image)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Base64.StartsWith("data:image/jpeg"))
            .WithMessage(localizer["ImageMustBeJpg"]);
    }
}