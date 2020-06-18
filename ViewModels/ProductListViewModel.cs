using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBakeryShop.Models;

namespace MyBakeryShop.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
        }
        public IEnumerable<Banner> Banners { get; internal set; }
        public IEnumerable<Product> Products { get; internal set; }
        //public string CurrentCategory { get; internal set; }
        public SelectList Style { get; set; }
        public string SearchString { get; set; }
        public string StyleString { get; set; }

    }
}