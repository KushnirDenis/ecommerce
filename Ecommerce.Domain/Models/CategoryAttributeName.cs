namespace Ecommerce.Domain.Models;

public class CategoryAttributeName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
    public int CategoryAttributeId { get; set; }
    public CategoryAttribute CategoryAttribute { get; set; }
}