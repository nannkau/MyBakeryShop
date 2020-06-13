using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<OderItem> ListProduct(int? id)

        {
            var product = from o in _bakeryDbContext.OrderDetails
                        join p in _bakeryDbContext.Products on o.ProductId equals p.ProductId
                        select new { p.ProductId, p.Name, o.Amount, o.Price};
            List < OderItem > oderItems= new List<OderItem>();
            foreach(var item in product)
            {
                OderItem od = new OderItem();
                od.ProductId = item.ProductId;
                od.Productname = item.Name;
                od.Amount = item.Amount;
                od.Price = item.Price;
                oderItems.Add(od);
            }
           // IEnumerable<OrderDetail> od = _bakeryDbContext.OrderDetails
             //                  .Where(t => t.OrderId==id);
            return oderItems;
        }
    }
}