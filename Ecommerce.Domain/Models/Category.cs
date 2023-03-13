namespace Ecommerce.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public List<CategoryName> Names { get; set; }
    public List<CategoryAttribute>? Attributes { get; set; }
    public CategoryImage Image { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryDiscount? Discount { get; set; }
    public List<Product>? Products { get; set; }
}