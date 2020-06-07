using Microsoft.AspNetCore.Mvc;
using MyBakeryShop.Models;
using MyBakeryShop.ViewModels;

namespace MyBakeryShop.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: /<controller>/
        public IActionResult List()
        {

            ProductListViewModel piesListViewModel = new ProductListViewModel();

            piesListViewModel.Products = _productRepository.GetAllProducts();

            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}