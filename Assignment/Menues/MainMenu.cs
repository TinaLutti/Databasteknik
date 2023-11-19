namespace Assignment.Menues;

public class MainMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly ProductMenu _productMenu;
    private readonly EmployeeMenu _employeeMenu;

    public MainMenu(CustomerMenu customerMenu, ProductMenu productMenu, EmployeeMenu employeeMenu)
    {
        _customerMenu = customerMenu;
        _productMenu = productMenu;
        _employeeMenu = employeeMenu;
    }

    public async Task StartAsync()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("-----Huvudmeny-----");
            Console.WriteLine("1. Hantera kunder");
            Console.WriteLine("2. Hantera produkter");
            Console.WriteLine("3. Hantera anställda");

            Console.Write("Välj ett alternativ: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _customerMenu.ManageCustomers();
                    break;

                case "2":
                    await _productMenu.ManageProducts();
                    break;

                case "3":
                    await _employeeMenu.ManageEmployees();
                    break;
            }
        }
        while (true);
    }





}

