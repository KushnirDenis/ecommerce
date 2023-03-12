namespace Ecommerce.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ShippingMethod ShippingMethod { get; set; }
    public string Address { get; set; }
    public double Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsFulfilled { get; set; } = false;
    public string IpAddress { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}