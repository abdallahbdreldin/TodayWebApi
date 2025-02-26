using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Models
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int id , string productName , string pictureUrl)
        {
            ProductItemId = id ;
            ProductName = productName ;
            PictureUrl = pictureUrl ;
        }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
