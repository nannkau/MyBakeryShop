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


    }
}