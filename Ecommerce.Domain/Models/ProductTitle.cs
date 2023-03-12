namespace Ecommerce.Domain.Models;

public class ProductTitle
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Language Language { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}