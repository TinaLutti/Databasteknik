using Assignment.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assignment.Repositories;

//gör generisk genom abstract av typen TEntity som är en klass
public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    protected Repo(DataContext context)
    {
        _context = context;
    }
    //CREATE tar emot en TEntity och returnerar tillbaka <>
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity ?? null!;
            //returnera tillbaka entiteten eller null
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return entity ?? null!;
    }
    //READ, heämta ALLA
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        // Om något går fel eller om undantag uppstår, returnera en tom lista av TEntity
        return Enumerable.Empty<TEntity>();
    }


    //Hämta en
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {

        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        return entity ?? null!;
    }
    //UPDATE
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity ?? null!;
    }
    //DELETE
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    // Kollar om det finns några entiteter i databasen som matchar uttrycket
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().AnyAsync(expression);

    }

}
