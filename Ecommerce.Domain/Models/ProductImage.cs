namespace Ecommerce.Domain.Models;

public class ProductImage
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}