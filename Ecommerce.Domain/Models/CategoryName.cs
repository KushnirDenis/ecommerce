namespace Ecommerce.Domain.Models;

public class CategoryName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public CategoryName()
    {
        
    }
    
    public CategoryName(string name, Language language)
    {
        Name = name;
        Language = language;
    }
    
    public CategoryName(int categoryId, string name, Language language)
    {
        CategoryId = categoryId;
        Name = name;
        Language = language;
    }
}