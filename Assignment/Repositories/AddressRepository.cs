using Assignment.Contexts;
using Assignment.Entities;

namespace Assignment.Repositories;

public class AddressRepository : Repo<AddressEntity>
{
    public AddressRepository(DataContext context) : base(context)
    {
    }
}
