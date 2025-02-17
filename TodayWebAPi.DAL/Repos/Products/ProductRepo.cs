using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Context;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;

namespace TodayWebAPi.DAL.Repos.Products
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly StoreContext _Context;

        public ProductRepo(StoreContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<IReadOnlyList<Product>> GetAllWithTypesAndBrandsAsync()
        {
            return await _Context.Products
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .ToListAsync();
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

        public async Task<int> SaveChangesAsync()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}
