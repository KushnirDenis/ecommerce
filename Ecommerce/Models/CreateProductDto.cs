using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class CreateProductDto
{
    public List<ProductTitleDto> Titles { get; set; }
    public List<ProductDescriptionDto> Descriptions { get; set; }
    public List<Image> Images { get; set; }
    public int CategoryId { get; set; }
    public double Price { get; set; }
}

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleForEach(c => c.Titles)
            .SetValidator(new ProductTitleDtoValidator(localizer));
        RuleForEach(c => c.Descriptions)
            .SetValidator(new ProductDescriptionDtoValidator(localizer));
        RuleForEach(c => c.Images)
            .SetValidator(new ImageValidator(localizer));
        RuleFor(c => c.Price)
            .Must(p => p >= 0);
    }
}