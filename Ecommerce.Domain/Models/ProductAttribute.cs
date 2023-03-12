namespace Ecommerce.Domain.Models;

public class ProductAttribute
{
    public int Id { get; set; }
    public ProductAttributeName Name { get; set; }
    public string Value { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryAttributeId { get; set; }
    public CategoryAttribute CategoryAttribute { get; set; }
}