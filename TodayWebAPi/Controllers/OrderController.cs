using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodayWebApi.BLL.Dtos;
using TodayWebApi.BLL.Managers;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IEmailManager _emailManager;
        public OrderController(IOrderManager orderManager , IEmailManager emailManager)
        {
            _orderManager = orderManager;
            _emailManager = emailManager;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return Unauthorized("User not authenticated");

            var order = await _orderManager.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId);

            if (order == null) return BadRequest("Order creation failed.");

            return Ok(order);
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUsers()
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return Unauthorized("User not authenticated");

            var orders = await _orderManager.GetOrdersForUserAsync(email);

            var ordersToReturn = orders.Select(order => new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                DeliveryMehtod = order.DeliveryMethod.ShortName,
                ShippingPrice = order.DeliveryMethod.Price,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ItemOrdered.ProductItemId,
                    ProductName = item.ItemOrdered.ProductName,
                    PictureUrl = item.ItemOrdered.PictureUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToHashSet(),
                SubTotal = order.SubTotal,
                Total = order.SubTotal + order.DeliveryMethod.Price,
                Status = order.Status.ToString()
            }).ToList();

            return Ok(ordersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = User?.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return Unauthorized("User not authenticated");

            var order = await _orderManager.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound("Order not found.");

            var orderToReturn = new OrderToReturnDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                DeliveryMehtod = order.DeliveryMethod.ShortName,
                ShippingPrice = order.DeliveryMethod.Price,
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ItemOrdered.ProductItemId,
                    ProductName = item.ItemOrdered.ProductName,
                    PictureUrl = item.ItemOrdered.PictureUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToHashSet(),
                SubTotal = order.SubTotal,
                Total = order.SubTotal + order.DeliveryMethod.Price,
                Status = order.Status.ToString()
            };

            return Ok(orderToReturn);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var methods = await _orderManager.GetDeliveryMethodsAsync();
            return Ok(methods);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus newStatus)
        {
            if(!Enum.IsDefined(typeof(OrderStatus), newStatus))
                return BadRequest("Invalid order status.");

            var order = await _orderManager.GetOrderByIdForAdminAsync(orderId);
            if (order == null) return NotFound("Order not found.");

            var success = await _orderManager.UpdateOrderStatusAsync(orderId, newStatus);
            if (!success) return BadRequest("Order status update failed.");

            
            await _emailManager.SendEmailAsync(order.BuyerEmail,
                $"Order #{orderId} Status Update",
                $"Your order status has been updated to: {newStatus}");

            return Ok(new { Message = "Order status updated successfully.", OrderId = orderId, NewStatus = newStatus.ToString() });
        }
    }
}

