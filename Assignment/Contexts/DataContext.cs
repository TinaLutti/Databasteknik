using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Contexts;

//Ärver fr DbContext
public class DataContext :DbContext
{
    //Konstruktor
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    //registrerar mina entiteter
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<PricingUnitEntity> PricingUnits { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }    
    
       
   }

