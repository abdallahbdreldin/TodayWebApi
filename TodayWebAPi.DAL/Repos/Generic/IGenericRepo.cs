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
    }
}
