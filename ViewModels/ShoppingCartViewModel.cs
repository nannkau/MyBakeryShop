using MyBakeryShop.Services;

namespace MyBakeryShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartService ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}