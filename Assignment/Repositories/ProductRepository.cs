using Assignment.Contexts;
using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories;
//Produktrepository  ärver från Repo<ProductEntity>
public class ProductRepository : Repo<ProductEntity>
{
    //vill komma åt contextdelen för att lagra DataContext
    private readonly DataContext _context;
   
    // Konstruktor tar emot och initierar DataContext
    public ProductRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    //Hämtar alla produkter med relaterade entiteter
    public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        // hämtar alla Products inkluderar entiteter (PricingUnit och ProductCategory)
        return await _context.Products
            .Include(x => x.PricingUnit)
            .Include(x => x.ProductCategory)
            .ToListAsync();
    }
}
