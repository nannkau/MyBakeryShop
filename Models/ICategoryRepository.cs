using System.Collections.Generic;
using MyBakeryShop.Models;

namespace MyBakeryShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}