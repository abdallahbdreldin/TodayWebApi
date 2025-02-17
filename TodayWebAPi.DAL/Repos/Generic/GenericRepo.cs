using TodayWebAPi.DAL.Data.Context;
using TodayWebAPi.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TodayWebAPi.DAL.Repos.Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseClass
{
    private readonly StoreContext _context;

    public GenericRepo(StoreContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T item)
    {
        await _context.AddAsync(item);
    }

    public async Task DeleteAsync(int id) 
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(); 
        }
    }

    public async Task UpdateAsync(T item)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync(); 
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

}
