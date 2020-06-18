using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBakeryShop.Models;
using MyBakeryShop.Models.Data;
using MyBakeryShop.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MyBakeryShop.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly BakeryDbContext _bakeryDbContext;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBannerRepository bannerRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _bannerRepository = bannerRepository;
        }

        // GET: /<controller>/
        
        public  IActionResult List(string styleString , string searchString)
        {
            // Use LINQ to get list of genres.


            ProductListViewModel piesListViewModel = new ProductListViewModel();
            piesListViewModel.Style = new SelectList(_productRepository.StyleList().ToList());
            piesListViewModel.Products =  _productRepository.SearchList(searchString, styleString);
            piesListViewModel.Banners = _bannerRepository.ListBanner();

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