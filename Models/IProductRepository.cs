using System.Collections.Generic;

namespace MyBakeryShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsOfTheWeek();
        IEnumerable<Banner> ListBanner();
        Product GetProductById(int pieId);
        IEnumerable<Product> SearchListAd(string searchString, string style);
        IEnumerable<Product> SearchList(string searchString,string style);
        IEnumerable<Product> SearchListCb(string searchString);
        IEnumerable<string> StyleList ();
        IEnumerable<Banner> Banners();
    }
}