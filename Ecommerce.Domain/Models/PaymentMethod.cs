namespace Ecommerce.Domain.Models;

public class PaymentMethod
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Language Language { get; set; }
}