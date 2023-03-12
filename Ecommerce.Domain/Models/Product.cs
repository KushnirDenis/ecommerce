namespace Ecommerce.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public ProductTitle Title { get; set; }
    public ProductDescription Description { get; set; }
    public List<ProductImage> Images { get; set; }
    public List<ProductAttribute> Attributes { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ProductDiscount? Discount { get; set; }
}