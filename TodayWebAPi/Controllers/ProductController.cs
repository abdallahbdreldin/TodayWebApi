using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodayWebApi.BLL.Dtos;
using TodayWebApi.BLL.Managers;

namespace TodayWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

       
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationDto paginationParams)
        {
            var (products, totalCount) = await _productManager.GetAllWithDetails(paginationParams.PageNumber, paginationParams.PageSize);

            var response = new
            {
                Data = products,
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize
            };

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> Search([FromQuery] string keyword)
        {
            var products = await _productManager.SearchProducts(keyword);
            return Ok(products);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterProducts(
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? inStock,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (products, totalCount) = await _productManager.FilterProducts(category, minPrice, maxPrice, inStock, pageNumber, pageSize);

            var response = new
            {
                Data = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productManager.GetProductWithDetailsAsync(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid product data");

            await _productManager.Add(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null || id != updateProductDto.Id)
                return BadRequest("Invalid product data");

            await _productManager.Update(updateProductDto);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productManager.GetProductWithDetailsAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            await _productManager.Remove(id);
            return Ok();
        }
    }
}
