using TodayWebAPi.DAL.Data.Context;
using TodayWebAPi.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

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
            }
        }

        public async Task UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
        }

        public async Task<T> GetEntityWithSpecAsync(Expression<Func<T, bool>> criteria,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (include != null) query = include(query);
            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (include != null) query = include(query);
            return await query.Where(criteria).ToListAsync();
        }


    }

}
