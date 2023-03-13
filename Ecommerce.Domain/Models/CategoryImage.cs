namespace Ecommerce.Domain.Models;

public class CategoryImage
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
}