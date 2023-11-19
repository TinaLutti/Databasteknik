using Assignment.Contexts;
using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories;

public class EmployeeRepository : Repo<EmployeeEntity>
{
    private readonly DataContext _context;

    public EmployeeRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // Överskuggning av funktionen i Repo för Employee, inkluderar Address
    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        // Använder Include för att inkludera Address för varje EmployeeEntity
        return await _context.Employees.Include(x => x.Address).ToListAsync();
    }
}