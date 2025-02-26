using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Basket;

namespace TodayWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _basketRepo;

        public BasketController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            if (!string.IsNullOrEmpty(basket.PaymentIntentId) && !basket.PaymentIntentId.StartsWith("pi_"))
            {
                
                Console.WriteLine($"Reset invalid PaymentIntentId '{basket.PaymentIntentId}' for BasketId={basket.Id}");
                basket.PaymentIntentId = null;
            }
            var updatedBasket = await _basketRepo.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _basketRepo.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}