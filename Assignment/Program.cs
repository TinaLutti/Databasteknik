using Assignment.Contexts;
using Assignment.Menues;
using Assignment.Repositories;
using Assignment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //registrerar min DataContext anv dependency injection 
            var services = new ServiceCollection();

            //Lägger till min SQL server
            services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Assignment\Assignment\Contexts\DataBase.mdf;Integrated Security=True;Connect Timeout=30"));

            //Registrerar mina Repositories, Menues, Services
            services.AddScoped<CustomerRepository>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<ProductRepository>();            
            services.AddScoped<ProductCategoryRepository>();
            services.AddScoped<PricingUnitRepository>();
            services.AddScoped<EmployeeRepository>();


            services.AddScoped<MainMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<ProductMenu>();
            services.AddScoped<EmployeeMenu>();

            services.AddScoped<ProductService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<EmployeeService>();


            // Bygger serviceprovider
            var sp = services.BuildServiceProvider();
            
            // Hämtar huvudmenyn/startar programmet
            var mainMenu = sp.GetRequiredService<MainMenu>();
            await mainMenu.StartAsync();
        }
    }
}