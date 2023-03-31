using Ecommerce.DAL;
using Ecommerce.Domain.Models;
using Ecommerce.Localization;
using Ecommerce.Models;
using FluentValidation;
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

    public CategoriesController(AppDbContext db,
        IStringLocalizer<ErrorMessages> localizer,
        IValidator<CreateCategoryDto> createCategoryDtoValidator)
    {
        _db = db;
        _localizer = localizer;
        _createCategoryDtoValidator = createCategoryDtoValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Language lang = Language.Ua)
    {
        var categories = _db.CategoryNames
            .Include(cn => cn.Category)
            .ThenInclude(c => c.Image)
            .Where(c => c.Language == lang && c.Category.ParentCategoryId == null)
            .Select(cn => CategoriesDto.MapFromCategoryName(cn))
            .ToList();

        GetCategories(categories, lang);

        return Ok(categories);
    }

    private List<CategoriesDto> GetCategories(List<CategoriesDto> categories, Language lang)
    {
        foreach (var category in categories)
        {
            var sub = _db.CategoryNames
                .Include(c => c.Category)
                .ThenInclude(c => c.Image)
                .Where(cn => cn.Category.ParentCategoryId == category.CategoryId &&
                             cn.Language == lang)
                .Select(cn => CategoriesDto.MapFromCategoryName(cn)).ToList();

            if (sub.Count == 0)
                return categories;
            category.Children.AddRange(GetCategories(sub, lang));
        }

        return categories;
    }

    // [HttpGet("{categoryId:int}/products")]
    // public async Task<IActionResult> GetProducts(int categoryId)
    // {
    //     
    // }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
    {
        var result = await _createCategoryDtoValidator.ValidateAsync(categoryDto);

        if (!result.IsValid)
            return BadRequest(new ErrorMessage(result.Errors
                .Select(e => e.ErrorMessage)));

        if (!await _db.Categories.AnyAsync(c => c.Id == categoryDto.ParentCategoryId))
            return BadRequest(new ErrorMessage(_localizer["Category"] +
                                               " " +
                                               _localizer["NotExists"]));

        foreach (var categoryName in categoryDto.Names)
        {
            if (await _db.CategoryNames.AnyAsync(c => c.Name == categoryName.Name &&
                                                      c.Language == categoryName.Language &&
                                                      c.Category.ParentCategoryId == categoryDto.ParentCategoryId))
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
            ParentCategoryId = categoryDto.ParentCategoryId,
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

        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), 
            "wwwroot/images/categories", filename);

        await categoryDto.Image.SaveToFile(imagePath);

        // TODO: url to created category
        return Created("", category);
    }
}