using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Context;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TodayWebAPi.DAL.Repos.Products
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly StoreContext _Context;

        public ProductRepo(StoreContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<(IReadOnlyList<Product>,int)> GetAllWithTypesAndBrandsAsync(int pageNumber, int pageSize)
        {
            var query = _Context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
            .AsQueryable();

            int totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToListAsync();
            return (products, totalCount);
        }

        public async Task<Product> GetProductWithDetailsAsync(int id)
        {
            return await _Context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IReadOnlyList<Product>> SearchProductsAsync(string keyword)
        {

            var products = await _Context.Products
               .FromSqlRaw("EXEC SearchProducts @Keyword = {0}", keyword)
               .ToListAsync();

            return products;
        }
        public async Task<(IReadOnlyList<Product>, int)> FilterProductsAsync(string category, decimal? minPrice, decimal? maxPrice, bool? inStock, int pageNumber, int pageSize)
        {
            var query = _Context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Type.Name.ToLower() == category.ToLower());
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (inStock.HasValue)
            {
                if (inStock.Value)
                {
                    query = query.Where(p => p.InStock > 0); 
                }
                else
                {
                    query = query.Where(p => p.InStock == 0);
                }
            }

            int totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<ProductBrand> GetBrandByNameAsync(string name)
        {
            return await _Context.ProductBrands.FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<ProductType> GetTypeByNameAsync(string name)
        {
            return await _Context.ProductTypes.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task AddBrandAsync(ProductBrand brand)
        {
            _Context.ProductBrands.Add(brand);
            await _Context.SaveChangesAsync();
        }

        public async Task AddTypeAsync(ProductType type)
        {
            _Context.ProductTypes.Add(type);
            await _Context.SaveChangesAsync();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
