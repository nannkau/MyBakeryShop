using System.Collections.Generic;
using MyBakeryShop.Models;

namespace MyBakeryShop.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
        }

        public IEnumerable<Product> Products { get; internal set; }
        //public string CurrentCategory { get; internal set; }
    }
}