using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Models
{
    public class OrderItem : BaseClass
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered itemOrdered , decimal price ,int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }
        public ProductItemOrdered ItemOrdered {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
