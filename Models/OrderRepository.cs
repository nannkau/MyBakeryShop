using System;
using System.Collections.Generic;
using MyBakeryShop.Models.Data;
using MyBakeryShop.Services;

namespace MyBakeryShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BakeryDbContext _bakeryDbContext;
        private readonly ShoppingCartService _shoppingCartService;

        public OrderRepository(BakeryDbContext bakeryDbContext, ShoppingCartService shoppingCartService)
        {
            _bakeryDbContext = bakeryDbContext;
            _shoppingCartService = shoppingCartService;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCartService.ShoppingCartItems;
            order.OrderTotal = _shoppingCartService.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.ProductId,
                    Price = shoppingCartItem.Product.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _bakeryDbContext.Orders.Add(order);

            _bakeryDbContext.SaveChanges();
        }
    }
}