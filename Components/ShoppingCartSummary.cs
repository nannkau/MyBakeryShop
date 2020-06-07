using Microsoft.AspNetCore.Mvc;
using MyBakeryShop.Services;
using MyBakeryShop.ViewModels;

namespace MyBakeryShop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartSummary(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IViewComponentResult Invoke()
        {

            var items = _shoppingCartService.GetShoppingCartItems();
            _shoppingCartService.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCartService,
                ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal()

            };

            return View(shoppingCartViewModel);
        }
    }
}