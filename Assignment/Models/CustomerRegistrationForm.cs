
namespace Assignment.Models;

// En modell för kundregistreringsformulär
// Samlar informationen nedan, och detta kommer att besvaras i CustomerMenu
public class CustomerRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}

