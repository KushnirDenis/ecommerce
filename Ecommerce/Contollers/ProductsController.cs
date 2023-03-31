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
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IValidator<CreateProductDto> _createProductValidator;

    public ProductsController(AppDbContext db,
        IStringLocalizer<ErrorMessages> localizer,
        IValidator<CreateProductDto> createProductValidator)
    {
        _db = db;
        _localizer = localizer;
        _createProductValidator = createProductValidator;
    }

    public async Task<IActionResult> Create(CreateProductDto productDto)
    {
        var validatonResult = await _createProductValidator.ValidateAsync(productDto);

        if (!validatonResult.IsValid)
            return BadRequest(new ErrorMessage(validatonResult.Errors
                .Select(e => e.ErrorMessage)));

        if (!await _db.Categories.AnyAsync(c => c.Id == productDto.CategoryId))
            return BadRequest(new ErrorMessage(_localizer["Category"] +
                                               " " +
                                               _localizer["NotExists"]));
        
        foreach (var productTitle in productDto.Titles)
        {
            if (await _db.ProductTitles.AnyAsync(pt => pt.Product.CategoryId == productDto.CategoryId &&
                                                       pt.Title == productTitle.Title &&
                                                       pt.Language == productTitle.Language
                ))
                return BadRequest(new ErrorMessage(_localizer["Product"] +
                                                   " " +
                                                   _localizer["AlreadyExists"]));
        }

        var titles = productDto.Titles
            .Select(t => t.ToProductTitle()).ToList();
        var descriptions = productDto.Descriptions
            .Select(t => t.ToProductDescription()).ToList();
        var images = productDto.Images
            .Select(i => new ProductImage() { Filename = 
                Guid.NewGuid() + "_" + i.Filename }).ToList();

        var product = new Product()
        {
            Titles = titles,
            Descriptions = descriptions,
            Price = productDto.Price,
            Images = images,
            CategoryId = productDto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        for (int i = 0; i < productDto.Images.Count; i++)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), 
                "wwwroot/images/products", images[i].Filename);

            await productDto.Images[i].SaveToFile(imagePath);
        }
        
        await _db.Products.AddAsync(product);
        
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ErrorMessage(_localizer["InternalError"]));
        }

        // TODO: url to created product
        return Created("", product);
    }
}