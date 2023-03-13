namespace Ecommerce.Models;

public class ErrorMessage
{
    public IEnumerable<string> Messages { get; set; }

    public ErrorMessage(IEnumerable<string> messages)
    {
        Messages = messages.Distinct();
    }
    public ErrorMessage(string message)
    {
        Messages = new List<string>
        {
            message
        };
    }
}