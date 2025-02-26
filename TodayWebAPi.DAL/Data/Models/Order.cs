using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Models
{
    public class Order : BaseClass
    {
        public Order()
        {
            
        }
        public Order(IReadOnlyList<OrderItem> orderItems , string buyerEmail ,DeliveryMethod deliveryMethod,decimal subtotal)
        {
            OrderItems = orderItems;
            BuyerEmail = buyerEmail;
            DeliveryMethod = deliveryMethod;
            SubTotal = subtotal;
        }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
