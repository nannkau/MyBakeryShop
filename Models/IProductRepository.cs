using System.Collections.Generic;

namespace MyBakeryShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsOfTheWeek();
        Product GetProductById(int pieId);
        IEnumerable<Product> SearchList(string searchString,string style);
        IEnumerable<string> StyleList ();
    }
}