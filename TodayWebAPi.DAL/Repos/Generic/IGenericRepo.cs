using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebAPi.DAL.Repos.Generic
{
    public interface IGenericRepo<T> where T : BaseClass
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(T item);
        

        Task<T> GetEntityWithSpecAsync(Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
