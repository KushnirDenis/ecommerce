namespace Ecommerce.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public CategoryName Name { get; set; }
    public List<CategoryAttribute>? Attributes { get; set; }
    public int? ParentCategoryId { get; set; }
    public CategoryDiscount? Discount { get; set; }
    public List<Product>? Products { get; set; }
}