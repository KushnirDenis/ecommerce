namespace Ecommerce.Domain.Models;

public class CategoryDiscount
{
    public int Id { get; set; }
    public double DiscountPercentage { get; set; }
    public DateTime Expires { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}