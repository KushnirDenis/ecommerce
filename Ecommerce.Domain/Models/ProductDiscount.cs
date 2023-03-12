namespace Ecommerce.Domain.Models;

public class ProductDiscount
{
    public int Id { get; set; }
    public double DiscountPercentage { get; set; }
    public DateTime Expires { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}