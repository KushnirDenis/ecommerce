using Ecommerce.DAL;
using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using Ecommerce.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Contollers;

[ApiController]
[ApiVersion("1")]
[Route("v{apiVer:apiVersion}/[controller]")]
public class CategoriesController : ControllerBase
{
    private AppDbContext _db;
    private IStringLocalizer<ErrorMessages> _localizer;
    private IValidator<CreateCategoryDto> _createCategoryDtoValidator;
    private IValidator<AddNameInAnotherLanguageDto> _addNameInAnotherLanguage;

    public CategoriesController(AppDbContext db,
        IStringLocalizer<ErrorMessages> localizer,
        IValidator<CreateCategoryDto> createCategoryDtoValidator,
        IValidator<AddNameInAnotherLanguageDto> addNameInAnotherLanguage)
    {
        _db = db;
        _localizer = localizer;
        _createCategoryDtoValidator = createCategoryDtoValidator;
    }

    // [HttpGet]
    // public IActionResult GetAll()
    // {
    //     _db.Ca
    // }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
    {
        var result = await _createCategoryDtoValidator.ValidateAsync(categoryDto);

        if (!result.IsValid)
            return BadRequest(new ErrorMessage(result.Errors
                .Select(e => e.ErrorMessage)));

        foreach (var categoryName in categoryDto.Names)
        {
            if (await _db.CategoryNames.AnyAsync(c => c.Name == categoryName.Name
                                                      && c.Language == categoryName.Language))
                return Conflict(new ErrorMessage(_localizer["Category"] +
                                                 " " +
                                                 _localizer["AlreadyExists"]));
        }

        var categoryNames = categoryDto.Names
            .Select(n => n.ToCategoryName());

        string filename = Guid.NewGuid() + "_" + categoryDto.Image.Filename;

        var category = new Category
        {
            Image = new CategoryImage
            {
                Filename = filename
            },
            Names = new List<CategoryName>(categoryNames)
        };

        await _db.Categories.AddAsync(category);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ErrorMessage(_localizer["InternalError"]));
        }

        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", filename);

        await categoryDto.Image.SaveToFile(imagePath);


        return Created("", category);
    }
}