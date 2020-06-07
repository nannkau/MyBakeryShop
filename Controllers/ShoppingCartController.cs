using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBakeryShop.Models;
using MyBakeryShop.Services;
using MyBakeryShop.ViewModels;

namespace MyBakeryShop_Shopping.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(ShoppingCartService shoppingCartService, IProductRepository productRepository)
        {
            _shoppingCartService = shoppingCartService;
            _productRepository = productRepository;
        }

        public ViewResult Index()
        {

            List<ShoppingCartItem> shoppingCartItems = _shoppingCartService.GetShoppingCartItems();
            _shoppingCartService.ShoppingCartItems = shoppingCartItems;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCartService,
                ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            Product selectedProduct = _productRepository.GetAllProducts().FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCartService.AddToCart(selectedProduct, 1);
            }

            return RedirectToAction("Index");

        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            Product selectedProduct = _productRepository.GetAllProducts().FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCartService.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");

        }
    }
}