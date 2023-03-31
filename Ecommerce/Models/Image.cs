using System.Data;
using Ecommerce.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Models;

public class Image
{
    public string Filename { get; set; }
    public string Base64 { get; set; }

    public async Task SaveToFile(string path) =>
        await File.WriteAllBytesAsync(path, Convert.FromBase64String(
            Base64.Remove(0, 23)));
    // remove "data:image/jpeg;base64,"
}

public class ImageValidator : AbstractValidator<Image>
{
    public ImageValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(c => c.Filename)
            .NotEmpty()
            .WithMessage(localizer["FilenameCannotBeEmpty"]);
        RuleFor(c => c.Base64)
            .NotEmpty()
            .WithMessage(localizer["FieldСannotBeEmpty"])
            .Must(x => x.StartsWith("data:image/jpeg"))
            .WithMessage(localizer["ImageMustBeJpg"]);
    }
}