namespace Assignment.Models;

public class ProductUpdateForm
{
    public string ProductDescription { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public string PricingUnit { get; set; } = null!;
    public string ProductCategory { get; set; } = null!;
}
