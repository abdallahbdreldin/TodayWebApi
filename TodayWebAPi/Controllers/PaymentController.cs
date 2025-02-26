using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodayWebApi.BLL.Managers;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;
        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        [HttpPost("basketId")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentManager.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
            {
                return BadRequest();
            }
            return Ok(basket);
        }
    }
}
