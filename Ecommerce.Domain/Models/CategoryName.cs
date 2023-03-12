namespace Ecommerce.Domain.Models;

public class CategoryName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}