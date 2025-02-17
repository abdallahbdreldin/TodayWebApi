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
        Task<IReadOnlyList<ProductDto>> GetAllWithDetails();
        Task<ProductDto> GetProductWithDetailsAsync(int id);
        Task<IReadOnlyList<ProductDto>> SearchProducts(string keyword);
        Task Add(ProductDto product);
        Task Remove(int id);
        Task Update(UpdateProductDto product);
    }
}
