namespace Assignment.Models;
// En modell för produktrefformulär
// Samlar informationen nedan, och detta kommer att besvaras i ProductMenu
public class ProductRegistrationForm
{
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public string PricingUnit { get; set; } = null!;
    public string ProductCategory { get; set; } = null!;
}