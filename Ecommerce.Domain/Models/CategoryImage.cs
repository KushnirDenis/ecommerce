namespace Ecommerce.Domain.Models;

public class CategoryImage
{
    public int CategoryId { get; set; }
    public string Filename { get; set; }
    public Category Category { get; set; }
}