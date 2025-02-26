using System.Collections.Generic;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;

namespace TodayWebAPi.DAL.Repos.Products
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<(IReadOnlyList<Product>,int)> GetAllWithTypesAndBrandsAsync(int pageNumber, int pageSize);
        Task<(IReadOnlyList<Product>, int)> FilterProductsAsync(string category, decimal? minPrice, decimal? maxPrice, bool? inStock, int pageNumber, int pageSize);
        Task<Product> GetProductWithDetailsAsync(int id);
        Task<IReadOnlyList<Product>> SearchProductsAsync(string keyword);
        Task<ProductBrand> GetBrandByNameAsync(string name);
        Task<ProductType> GetTypeByNameAsync(string name);
        Task AddBrandAsync(ProductBrand brand);
        Task AddTypeAsync(ProductType type);
        Task<int> SaveChangesAsync();
    }
}
