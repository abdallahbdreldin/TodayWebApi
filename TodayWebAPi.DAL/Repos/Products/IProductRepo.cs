using System.Collections.Generic;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;

namespace TodayWebAPi.DAL.Repos.Products
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<IReadOnlyList<Product>> GetAllWithTypesAndBrandsAsync();
        Task<Product> GetProductWithDetailsAsync(int id);
        Task<IReadOnlyList<Product>> SearchProductsAsync(string keyword);

        Task<int> SaveChangesAsync();
    }
}
