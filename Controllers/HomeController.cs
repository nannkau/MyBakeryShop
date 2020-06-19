using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBakeryShop.Models;
using MyBakeryShop.ViewModels;
//ok
namespace MyBakeryShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IBannerRepository _bannerRepository;

        public HomeController(IProductRepository productRepository, IBannerRepository bannerRepository)
        {
            _productRepository = productRepository;
            _bannerRepository = bannerRepository;
        }


        public IActionResult Index()
        {

            ProductListViewModel productListViewModel = new ProductListViewModel();

            productListViewModel.Products = _productRepository.GetProductsOfTheWeek();
            productListViewModel.Banners = _bannerRepository.ListBanner();

            return View(productListViewModel);

        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
