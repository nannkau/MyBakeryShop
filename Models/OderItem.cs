using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.Models
{
    public class OderItem
    {
        public int ProductId { get; set; }
        public string Productname { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

    }
}
