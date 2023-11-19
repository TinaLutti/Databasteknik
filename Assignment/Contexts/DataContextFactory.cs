using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Assignment.Contexts;

//Skapa instanser av DataContext för designtidsskapande
public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    // Metod för att skapa en instans av DataContext för desig 
    public DataContext CreateDbContext(string[] args)
    {
        //Registrerar min Sql Server
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Assignment\Assignment\Contexts\DataBase.mdf;Integrated Security=True;Connect Timeout=30");
        return new DataContext(optionsBuilder.Options);
    }
}
