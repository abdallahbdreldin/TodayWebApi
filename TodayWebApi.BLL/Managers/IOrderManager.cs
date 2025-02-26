using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebApi.BLL.Managers
{
    public interface IOrderManager
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<Order> GetOrderByIdForAdminAsync(int id);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    }
}
