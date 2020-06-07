using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBakeryShop.Models;
using MyBakeryShop.Services;

namespace MyBakeryShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCartService _shoppingCartService;

        public OrderController(IOrderRepository orderRepository, ShoppingCartService shoppingCartService)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCartService.GetShoppingCartItems();
            _shoppingCartService.ShoppingCartItems = items;

            if (_shoppingCartService.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCartService.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies!";
            return View();
        }
    }
}