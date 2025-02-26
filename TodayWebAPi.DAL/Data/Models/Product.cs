using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Models
{
    public class Product : BaseClass
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        public int InStock { get; set; }
        public  ProductBrand Brand { get; set; }
        public  int ProductBrandId { get; set; }
        public ProductType Type { get; set; }
        public int ProductTypeId { get; set; }
    }
}
