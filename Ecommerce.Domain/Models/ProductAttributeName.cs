namespace Ecommerce.Domain.Models;

public class ProductAttributeName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
    public int ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; }
}