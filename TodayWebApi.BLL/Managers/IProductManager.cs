using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebApi.BLL.Dtos;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebApi.BLL.Managers
{
    public interface IProductManager
    {
        Task<(IReadOnlyList<ProductDto>, int)> GetAllWithDetails(int pageNumber, int pageSize);
        Task<(IReadOnlyList<ProductDto>, int)> FilterProducts(string category, decimal? minPrice, decimal? maxPrice, bool? inStock, int pageNumber, int pageSize);
        Task<ProductDto> GetProductWithDetailsAsync(int id);
        Task<IReadOnlyList<ProductDto>> SearchProducts(string keyword);
        Task Add(ProductDto product);
        Task Remove(int id);
        Task Update(UpdateProductDto product);
    }
}
