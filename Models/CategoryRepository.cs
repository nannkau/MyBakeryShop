using System.Collections.Generic;
using MyBakeryShop.Models.Data;

namespace MyBakeryShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;

        public CategoryRepository(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _bakeryDbContext.Categories;
        }
    }
}