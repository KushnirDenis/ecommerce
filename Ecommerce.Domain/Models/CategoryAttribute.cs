namespace Ecommerce.Domain.Models;

public class CategoryAttribute
{
    public int Id { get; set; }
    public CategoryAttributeName Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<ProductAttribute>? ProductAttributes { get; set; }
}