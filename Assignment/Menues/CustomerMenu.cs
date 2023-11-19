using Assignment.Models;
using Assignment.Services;

namespace Assignment.Menues;

public class CustomerMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ManageCustomers()
    {
        Console.Clear();
        Console.WriteLine("---Hantera kunder---");
        Console.WriteLine("1. Se alla kunder");
        Console.WriteLine("2. Lägg till kund");
        Console.WriteLine("3. Se en kund");
        Console.WriteLine("4. Ta bort kund");
        Console.WriteLine("5. Updatera kund");
        Console.WriteLine("0. Huvudmeny");
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
                await GetAsync();
                break;

            case "4":
                await DeleteAsync();
                break;

            case "5":
                await UpdateAsync();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Felaktigt val.");
                break;
        }
    }

    public async Task ListAllAsync()
    {
        Console.Clear();

        var customers = await _customerService.GetAllAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            Console.WriteLine($"{customer.Address.StreetName}, {customer.Address.PostalCode} {customer.Address.City}");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }

    public async Task CreateAsync()
    {
        var form = new CustomerRegistrationForm();

        Console.Clear();
        Console.Write("Förnamn: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write("Efternamn: ");
        form.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write("Gatunamn: ");
        form.StreetName = Console.ReadLine()!;

        Console.Write("Postkod: ");
        form.PostalCode = Console.ReadLine()!;

        Console.Write("Stad: ");
        form.City = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(form);
        if (result)
            Console.WriteLine("Kund skapad.");
        else
            Console.WriteLine("Kunde inte skapa kund.");
    }
    public async Task GetAsync()
    {
        Console.Clear();

        // Hämta e-postadress från användaren
        Console.WriteLine("Ange email för att se kund: ");
        var email = Console.ReadLine();

        // Hämta medlem baserat på e-postadress
        var customer = await _customerService.GetAsync(email!);

        // Kontrollera om medlemmen finns
        if (customer != null)
        {
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            // Kontrollera om Address är null innan du försöker komma åt dess egenskaper
            if (customer.Address != null)
            {
                Console.WriteLine($"{customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City}");
            }
            else
            {
                Console.WriteLine("Kunden hittades inte.");
            }
        }
        else
        {
            Console.WriteLine("Kunden hittades inte.");
        }

        //För att hinna läsa listan
        Console.ReadKey();
    }
    //DELETE
    public async Task<bool> DeleteAsync()
    {
        Console.Clear();

        // Hämta e-postadress från användaren
        Console.WriteLine("Ange email för att ta bort kund: ");
        var email = Console.ReadLine();

        // Anropa DeleteAsync från CustomerService för att ta bort medlemmen

        var result = await _customerService.DeleteAsync(email!);

        if (result)
        {
            Console.WriteLine("Kund borttagen.");
        }
        else
        {
            Console.WriteLine("Kunde inte ta bort kund.");
        }

        // För att hinna läsa meddelandet
        Console.ReadKey();

        // Returnera resultatet
        return result;
    }
    //UPDATE    

    public async Task UpdateAsync()
    {
        Console.Clear();

        // Hämta e-postadress från kunden
        Console.WriteLine("Ange email för att uppdatera kund: ");
        var email = Console.ReadLine();

        // Hämta kunden baserat på e-postadress
        var customer = await _customerService.GetAsync(email!);

        if (customer != null)
        {
            Console.WriteLine($"Nuvarande kunduppgifter: {customer.FirstName} {customer.LastName}");

            // Kontrollera om Address är null innan du försöker komma åt dess egenskaper
            if (customer.Address != null)
            {
                Console.WriteLine($"{customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City}");
            }
            else
            {
                Console.WriteLine("Ingen andress tillgängleg för denna kund.");
            }

            // Låt användaren mata in uppdaterade uppgifter
            var form = new CustomerUpdateForm();
            Console.WriteLine("Ange uppdaterad information:");

            Console.Write("Förnamn: ");
            form.FirstName = Console.ReadLine()!;

            Console.Write("Efternamn: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("Gatunamn: ");
            form.StreetName = Console.ReadLine()!;

            Console.Write("Postkod: ");
            form.PostalCode = Console.ReadLine()!;

            Console.Write("stad: ");
            form.City = Console.ReadLine()!;

            // Uppdatera kunden
            var result = await _customerService.UpdateCustomerAsync(form, email!);

            if (result)
            {
                Console.WriteLine("Kund uppdaterad.");
            }
            else
            {
                Console.WriteLine("Kunde inte uppdatera kund.");
            }
        }
        else
        {
            Console.WriteLine("Kund hittades inte.");
        }

        // För att hinna läsa meddelandet
        Console.ReadKey();
    }

}
