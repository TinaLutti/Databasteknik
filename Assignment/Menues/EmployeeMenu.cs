using Assignment.Models;
using Assignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Menues;

public class EmployeeMenu
{
    
    private readonly EmployeeService _employeeService;

    public EmployeeMenu(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task ManageEmployees()
    {
        Console.Clear();
        Console.WriteLine("---Hantera Anställda---");
        Console.WriteLine("1. Se alla anställda");
        Console.WriteLine("2. Lägg till anställd");
        Console.WriteLine("3. Se en anställd");
        Console.WriteLine("4. Ta bort anställd");
        Console.WriteLine("5. Updatera anställd");
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

        var employees = await _employeeService.GetAllAsync();
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            Console.WriteLine($"{employee.Address.StreetName}, {employee.Address.PostalCode} {employee.Address.City}");
            Console.WriteLine("");
        }

        Console.ReadKey();
    }

    public async Task CreateAsync()
    {
        var form = new EmployeeRegistrationForm();

        Console.Clear();
        Console.Write("Förnamn: ");
        form.FirstName = Console.ReadLine()!;

        Console.Write("Efternamn: ");
        form.LastName = Console.ReadLine()!;

        Console.Write("Lön: ");
        form.Salary = decimal.Parse(Console.ReadLine()!);

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        Console.Write("Gatunamn: ");
        form.StreetName = Console.ReadLine()!;

        Console.Write("Postkod: ");
        form.PostalCode = Console.ReadLine()!;

        Console.Write("Stad: ");
        form.City = Console.ReadLine()!;

        var result = await _employeeService.CreateEmployeeAsync(form);
        if (result)
            Console.WriteLine("Anställd skapad.");
        else
            Console.WriteLine("Kunde inte skapa anställd.");
    }
    public async Task GetAsync()
    {
        Console.Clear();

        // Hämta e-postadress från anställd
        Console.WriteLine("Ange email för att se anställd: ");
        var email = Console.ReadLine();

        // Hämta anställd baserat på e-postadress
        var employee = await _employeeService.GetAsync(email!);

        // Kontrollera om anställd finns
        if (employee != null)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            // Kontrollera om Address är null innan du försöker komma åt dess egenskaper
            if (employee.Address != null)
            {
                Console.WriteLine($"{employee.Address.StreetName} {employee.Address.PostalCode} {employee.Address.City} {employee.Salary}");
            }
            else
            {
                Console.WriteLine("Anställd hittades inte.");
            }
        }
        else
        {
            Console.WriteLine("Anställd hittades inte.");
        }

        //För att hinna läsa listan
        Console.ReadKey();
    }
    //DELETE
    public async Task<bool> DeleteAsync()
    {
        Console.Clear();

       
        Console.WriteLine("Ange email för att ta bort anställd: ");
        var email = Console.ReadLine();


        var result = await _employeeService.DeleteAsync(email!);

        if (result)
        {
            Console.WriteLine("Anställd borttagen.");
        }
        else
        {
            Console.WriteLine("Kunde inte ta bort anställd.");
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

       
        Console.WriteLine("Ange email för att uppdatera anställd: ");
        var email = Console.ReadLine();

        
        var employee = await _employeeService.GetAsync(email!);

        if (employee != null)
        {
            Console.WriteLine($"Nuvarande kunduppgifter: {employee.FirstName} {employee.LastName}");

            // Kontrollera om Address är null innan du försöker komma åt dess egenskaper
            if (employee.Address != null)
            {
                Console.WriteLine($"{employee.Address.StreetName} {employee.Address.PostalCode} {employee.Address.City} {employee.Salary}");
            }
            else
            {
                Console.WriteLine("Ingen andress tillgänglig för denna anställda.");
            }

            // Låt användaren mata in uppdaterade uppgifter
            var form = new EmployeeUpdateForm();
            Console.WriteLine("Ange uppdaterad information:");

            Console.Write("Förnamn: ");
            form.FirstName = Console.ReadLine()!;

            Console.Write("Efternamn: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("Lön: ");
            form.Salary = decimal.Parse(Console.ReadLine()!);

            Console.Write("Gatunamn: ");
            form.StreetName = Console.ReadLine()!;

            Console.Write("Postkod: ");
            form.PostalCode = Console.ReadLine()!;

            Console.Write("stad: ");
            form.City = Console.ReadLine()!;

            // Uppdatera kunden
            var result = await _employeeService.UpdateEmployeeAsync(form, email!);

            if (result)
            {
                Console.WriteLine("Anställd uppdaterad.");
            }
            else
            {
                Console.WriteLine("Kunde inte uppdatera anställd.");
            }
        }
        else
        {
            Console.WriteLine("Anställd hittades inte.");
        }

        // För att hinna läsa meddelandet
        Console.ReadKey();
    }
}
