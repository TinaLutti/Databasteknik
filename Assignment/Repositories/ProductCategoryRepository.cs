using Assignment.Contexts;
using Assignment.Entities;
namespace Assignment.Repositories;

public class ProductCategoryRepository : Repo<ProductCategoryEntity>
{
    public ProductCategoryRepository(DataContext context) : base(context)
    {
    }
}
