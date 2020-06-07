using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyBakeryShop.Models.Data;

namespace MyBakeryShop.Models
{
    public class ProductRepository : IProductRepository
    {

        private readonly BakeryDbContext _bakeryDbContext;

        public ProductRepository(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _bakeryDbContext.Products.Include(c => c.Category);
        }

        public Product GetProductById(int pieId)
        {
            return _bakeryDbContext.Products.FirstOrDefault(p => p.ProductId == pieId);
        }

        public IEnumerable<Product> GetProductsOfTheWeek()
        {
            return _bakeryDbContext.Products.Include(c => c.Category).Where(p => p.IsProductOfTheWeek);
        }

        public IEnumerable<Product> SearchList(string searchString, string style)
        {
            var list = from p in _bakeryDbContext.Products
                       join c in _bakeryDbContext.Categories on p.CategoryId equals c.CategoryId
                       select new { p.ProductId, p.Name, p.LongDescription, p.ShortDescription, p.Price, p.AllergyInformation, p.ImageUrl, p.ImageThumbnailUrl, p.IsProductOfTheWeek, p.InStock, c.CategoryId, c.CategoryName, c.Description };
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.Name.Contains(searchString));
            }

            //4. Tìm kiếm theo CategoryID
            if (!string.IsNullOrEmpty(style))
            {
                list = list.Where(x => x.Name.Contains(style));
            }

            //5. Chuyển đổi kết quả từ var sang danh sách List<Link>
            List<Product> products = new List<Product>() ;
            foreach (var item in list)
            {
                Product temp = new Product();
                temp.ProductId = item.ProductId;
                temp.ShortDescription = item.ShortDescription;
                temp.Price = item.Price;
                temp.Name = item.Name;
                temp.LongDescription = item.LongDescription;
                temp.InStock = item.InStock;
                temp.ImageThumbnailUrl = item.ImageThumbnailUrl;
                temp.ImageUrl = item.ImageUrl;
                products.Add(temp);
            }                                                                           
            return products;

        }

        public IEnumerable<string> StyleList()
        {
            IEnumerable<string> genreQuery = from m in _bakeryDbContext.Categories
                                            orderby m.CategoryName
                                            select m.CategoryName;
            return genreQuery;
        }
    }
}