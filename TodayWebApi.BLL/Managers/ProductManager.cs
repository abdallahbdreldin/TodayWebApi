using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebApi.BLL.Dtos;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Products;

namespace TodayWebApi.BLL.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepo _repo;
        public ProductManager(IProductRepo repo)
        {
            _repo = repo;
        }
        public async Task Add(ProductDto product)
        {
            Product productDb = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
            };

            await _repo.AddAsync(productDb);
            await _repo.SaveChangesAsync(); 
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllWithDetails()
        {
            var products = await _repo.GetAllWithTypesAndBrandsAsync(); 

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
                ProductType = p.Type.Name, 
                ProductBrand = p.Brand.Name 
            }).ToList();

            return productDtos;
        }

        public async Task<ProductDto> GetProductWithDetailsAsync(int id)
        {
            var product = await _repo.GetProductWithDetailsAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductType = product.Type.Name, 
                ProductBrand = product.Brand.Name 
            };
        }
        public async Task<IReadOnlyList<ProductDto>> SearchProducts(string keyword)
        {
            var products = await _repo.SearchProductsAsync(keyword);

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
                ProductType = p.Type?.Name,  
                ProductBrand = p.Brand?.Name  
            }).ToList();
        }


        public async Task Update(UpdateProductDto product)
        {
            var productDb = await _repo.GetProductWithDetailsAsync(product.Id);

            if (productDb == null)
            {
                return; 
            }

            
            productDb.Name = product.Name;
            productDb.Description = product.Description;
            productDb.Price = product.Price;
            productDb.PictureUrl = product.PictureUrl;

            await _repo.SaveChangesAsync(); 
        }
        public async Task Remove(int id)
        {
            await _repo.DeleteAsync(id); 
            await _repo.SaveChangesAsync();
        }

    }
}
