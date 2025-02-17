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
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
            var products = await _productManager.GetAllWithDetails();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> Search([FromQuery] string keyword)
        {
            var products = await _productManager.SearchProducts(keyword);
            return Ok(products);
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
            return NoContent();
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
            return NoContent();
        }
    }
}
