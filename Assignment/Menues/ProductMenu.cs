using Assignment.Models;
using Assignment.Services;

namespace Assignment.Menues;

// Meny som hanterar produkter med hjälp av ProductService
public class ProductMenu
{
    //Lagrar ProductService
    private readonly ProductService _productService;

    //Tar emot och initierar ProductService
    public ProductMenu(ProductService productService)
    {
        _productService = productService;
    }

    // Huvudmenyn för produkter
    public async Task ManageProducts()
    {
        // Rensa konsolfönstret och skriv ut huvudmenyn i konsollen
        Console.Clear();
        Console.WriteLine("Hantera produkter");
        Console.WriteLine("1. Se alla produkter");
        Console.WriteLine("2. Skapa produkt ");
        Console.WriteLine("3. Se alla prisenheter");
        Console.WriteLine("4. Se en produkt");
        Console.WriteLine("5. Updatera en produkt");
        Console.WriteLine("6. Ta bort en produkt");
        Console.WriteLine("0. Huvudmeny");

        // Invänta användarens val
        Console.Write("Välj ett alternativ: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await ListAllAsync();
                break;

            case "2":
                await CreateAsync();
                break;

            case "3":
                await ListAllPricingUnitsAsync();
                break;

            case "4":
                await GetAsync();
                break;

            case "5":
                await UpdateAsync();
                break;

            case "6":
                await DeleteAsync();
                break;



            case "0":
                return;

            default:
                Console.WriteLine("Felaktigt val.");
                break;
        }
    }

    // Visa alla produkter
    public async Task ListAllAsync()
    {
        Console.Clear();
        // Hämta alla produkter från ProductService
        var products = await _productService.GetAllAsync();
        foreach (var product in products)
        {
            // Skriv ut informationen 
            Console.WriteLine($"{product.ProductName} ({product.ProductCategory.CategoryName})");
            Console.WriteLine($"{product.ProductPrice} ({product.PricingUnit.Unit})");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }
    // Skapa en ny produkt 
    public async Task CreateAsync()
    {
        var form = new ProductRegistrationForm();

        Console.Clear();
        Console.Write("Produknamn: ");
        form.ProductName = Console.ReadLine()!;

        Console.Write("Beskrivning: ");
        form.ProductDescription = Console.ReadLine()!;

        Console.Write("Produktkategori: ");
        form.ProductCategory = Console.ReadLine()!;

        Console.Write("Pris (SEK): ");
        form.ProductPrice = decimal.Parse(Console.ReadLine()!);

        Console.Write("Prisenhet (st/pkt/tim): ");
        form.PricingUnit = Console.ReadLine()!;
       
        // Anropa ProductService för att skapa produkten
        var result = await _productService.CreateProductAsync(form);
        if (result)
            Console.WriteLine("Produkt skapad.");
        else
            Console.WriteLine("Kunde inte skapa produkt.");
    }

   
    // Visa alla prisenheter
    public async Task ListAllPricingUnitsAsync()
    {
        Console.Clear();

        var units = await _productService.GetAllPricingUnitsAsync();
        foreach (var unit in units)
        {
            Console.WriteLine($"{unit.Unit}");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }

    public async Task GetAsync()
    {
        Console.Clear();

        // Hämta produktnamn
        Console.WriteLine("Produktnamn: ");
        var productName = Console.ReadLine();

        // Hämta medlem baserat på produktnamn
        var product = await _productService.GetAsync(productName!);

        // Kontrollera om produkt finns
        if (product != null)
        {
            Console.WriteLine($"{product.ProductName} {product.ProductDescription}");

        }
        else
        {
            Console.WriteLine("Produkten hittades inte.");
        }
     

        //För att hinna läsa listan
        Console.ReadKey();
    }
    //UPDATE    

    public async Task UpdateAsync()
    {
        Console.Clear();

        
        Console.WriteLine("Ange produktnamn: ");
        var productName = Console.ReadLine();

        // Hämta productnamn
        var product = await _productService.GetAsync(productName!);

        if (product != null)
        {
            Console.WriteLine($"Nuvarande produktuppgifter: {product.ProductCategory.CategoryName} {product.PricingUnit.Unit} {product.ProductDescription}");

            // Kontrollera om categori är null innan du försöker komma åt dess egenskaper
            if (product.ProductCategory != null)
            {
                Console.WriteLine($"{product.ProductCategory.CategoryName}");
            }
            else
            {
                Console.WriteLine("Ingen kategori tillgänglig för denna produkt.");
            }
            if (product.PricingUnit.Unit != null)
            {
                Console.WriteLine($"{product.PricingUnit.Unit}");
            }
            else
            {
                Console.WriteLine("Ingen prisenhet tillgänglig för denna produkt.");
            }

            // Låt användaren mata in uppdaterade uppgifter
            var form = new ProductUpdateForm();
            Console.WriteLine("Ange uppdaterad information:");

            Console.Write("Kategori: ");
            form.ProductCategory = Console.ReadLine()!;
                        
            Console.Write("Pris: ");
            string priceInput = Console.ReadLine()!;

            if (decimal.TryParse(priceInput, out decimal productPrice))
            {
                // Får 'productPrice' som en decimal som du kan använda i din kod.
                form.ProductPrice = productPrice;
            }
            else
            {
                Console.WriteLine("Ogiltig. Ange ett giltigt decimaltal.");
            }


            Console.Write("Prisenhet: ");
            form.PricingUnit = Console.ReadLine()!;

            Console.Write("Beskrivning: ");
            form.ProductDescription = Console.ReadLine()!;

           
            // Uppdatera produkten
            var result = await _productService.UpdateProductAsync(form, productName!);

            if (result)
            {
                Console.WriteLine("Produkten är uppdaterad.");
            }
            else
            {
                Console.WriteLine("Kunde inte uppdatera produkten.");
            }
        }
        else
        {
            Console.WriteLine("Produkt hittades inte.");
        }

        // För att hinna läsa meddelandet
        Console.ReadKey();
    }
    //DELETE
    public async Task<bool> DeleteAsync()
    {
        Console.Clear();

        // Hämta productnamn
        Console.WriteLine("Ange namn på produkten du vill ta bort: ");
        var productName = Console.ReadLine();

        // Anropa DeleteAsync från ProductService för att ta bort produkten

        var result = await _productService.DeleteAsync(productName!);

        if (result)
        {
            Console.WriteLine("Produkt borttagen.");
        }
        else
        {
            Console.WriteLine("Kunde inte ta bort produkt.");
        }

        // För att hinna läsa meddelandet
        Console.ReadKey();

        // Returnera resultatet
        return result;
    }
}

