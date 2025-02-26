using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Basket;
using TodayWebAPi.DAL.Repos.Generic;
using TodayWebAPi.DAL.Repos.UnitOfWork;

namespace TodayWebApi.BLL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepo _basketrepo;

        public OrderManager(IUnitOfWork unitOfWork, IBasketRepo basketrepo)
        {
            _unitOfWork = unitOfWork;
            _basketrepo = basketrepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId)
        {
            var basket = await _basketrepo.GetBasketAsync(basketId);
            if (basket == null || !basket.Items.Any())
            {
                return null; 
            }
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repo<Product>().GetByIdAsync(item.Id);
                if (productItem == null) continue;

                var itemOrdered = new ProductItemOrdered(productItem.Id,productItem.Name,productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repo<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            if (deliveryMethod == null)
            {
                return null;
            }

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items,buyerEmail,deliveryMethod,subTotal);

            await _unitOfWork.Repo<Order>().AddAsync(order);

            var result = await _unitOfWork.Complete();

            await _basketrepo.DeleteBasketAsync(basketId);

            if (result <= 0) return null;

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repo<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            return await _unitOfWork.Repo<Order>()
            .GetEntityWithSpecAsync(o => o.Id == id && o.BuyerEmail == buyerEmail,
                include: query => query
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod));
        }
        public async Task<Order> GetOrderByIdForAdminAsync(int id)
        {
            return await _unitOfWork.Repo<Order>()
            .GetEntityWithSpecAsync(o => o.Id == id,
                include: query => query
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.ItemOrdered)
                .Include(o => o.DeliveryMethod));
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _unitOfWork.Repo<Order>()
            .GetAllWithSpecAsync(o => o.BuyerEmail == buyerEmail,
                include: query => query
                .Include(o => o.OrderItems)
                .Include(o => o.DeliveryMethod));
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _unitOfWork.Repo<Order>().GetByIdAsync(orderId);

            if (order == null) return false; 

            order.Status = newStatus;

            _unitOfWork.Repo<Order>().UpdateAsync(order);

            var result = await _unitOfWork.Complete();

            return result > 0;
        }
    }
}
