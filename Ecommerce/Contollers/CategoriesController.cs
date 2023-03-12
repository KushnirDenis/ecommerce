using Ecommerce.DAL;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Contollers;

[ApiController]
[ApiVersion("1")]
[Route("{apiVer:apiVersion}/[controller]")]
public class CategoriesController : ControllerBase
{
    private AppDbContext _db;
    private IStringLocalizer _localizer;

    public CategoriesController(AppDbContext db, IStringLocalizer localizer)
    {
        _db = db;
        _localizer = localizer;
    }

    // [HttpGet]
    // public IActionResult GetAll()
    // {
    //     _db.Ca
    // }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryDto categoryDto)
    {
        return Ok(_localizer["FieldСannotBeEmpty"]);
    }
}