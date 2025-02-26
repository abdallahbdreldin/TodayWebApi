using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodayWebApi.BLL.Dtos;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Products;




namespace TodayWebApi.BLL.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepo _repo;
        public ProductManager(IProductRepo repo )
        {
            _repo = repo;
        }

        public async Task Add(ProductDto product)
        {
            
            var brand = await _repo.GetBrandByNameAsync(product.ProductBrand);
            var type = await _repo.GetTypeByNameAsync(product.ProductType);

            
            if (brand == null)
            {
                brand = new ProductBrand { Name = product.ProductBrand };
                await _repo.AddBrandAsync(brand);
            }

            
            if (type == null)
            {
                type = new ProductType { Name = product.ProductType };
                await _repo.AddTypeAsync(type);
            }


            var productDb = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                InStock = product.InStock,
                ProductBrandId = brand.Id,
                ProductTypeId = type.Id
            };

            await _repo.AddAsync(productDb);
            await _repo.SaveChangesAsync();
        }

        public async Task<(IReadOnlyList<ProductDto>, int)> GetAllWithDetails(int pageNumber, int pageSize)
        {
            var (products, totalCount) = await _repo.GetAllWithTypesAndBrandsAsync(pageNumber, pageSize);

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
                InStock = p.InStock,
                ProductType = p.Type.Name,
                ProductBrand = p.Brand.Name
            }).ToList();

            return (productDtos, totalCount);
        }

        public async Task<ProductDto> GetProductWithDetailsAsync(int id)
        {
            var product = await _repo.GetProductWithDetailsAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                InStock = product.InStock,
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
                InStock = p.InStock,
                ProductType = p.Type?.Name,
                ProductBrand = p.Brand?.Name
            }).ToList();
        }
        public async Task<(IReadOnlyList<ProductDto>, int)> FilterProducts(string category, decimal? minPrice, decimal? maxPrice, bool? inStock, int pageNumber, int pageSize)
        {
            var (products, totalCount) = await _repo.FilterProductsAsync(category, minPrice, maxPrice, inStock, pageNumber, pageSize);

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
                ProductType = p.Type.Name,
                ProductBrand = p.Brand.Name,
                InStock = p.InStock
            }).ToList();

            return (productDtos, totalCount);
        }

        public async Task Update(UpdateProductDto product)
        {
            var productDb = await _repo.GetProductWithDetailsAsync(product.Id);

            if (productDb == null)
                return;

            productDb.Name = product.Name;
            productDb.Description = product.Description;
            productDb.Price = product.Price;
            productDb.PictureUrl = product.PictureUrl;
            productDb.InStock = product.InStock;

            await _repo.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
        }

        
    }
}
