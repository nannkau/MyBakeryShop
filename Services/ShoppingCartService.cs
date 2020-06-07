using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBakeryShop.Models;
using MyBakeryShop.Models.Data;

namespace MyBakeryShop.Services
{
    public class ShoppingCartService
    {
        private readonly BakeryDbContext _bakeryDbContext;

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartService(BakeryDbContext myBakeryDbContext)
        {
            this._bakeryDbContext = myBakeryDbContext;
        }

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            // What is happening here ??
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            // Get DB Context
            var context = services.GetService<BakeryDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartService(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem =
                    _bakeryDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _bakeryDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bakeryDbContext.SaveChanges();
        }


        public int RemoveFromCart(Product product)
        {
            // Get the shopping cart item from the DB (via the DBContext)
            var shoppingCartItem =
                    _bakeryDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            // if the product is there in the shopping cart.
            if (shoppingCartItem != null)
            {
                // if there is more than one in the shopping cart
                if (shoppingCartItem.Amount > 1)
                {
                    // reduce the item count and reduce the total.
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    // if its only one item remove it from shopping cart.
                    _bakeryDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bakeryDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // This is how inner join works in EFCore.
            return ShoppingCartItems ??
                    (ShoppingCartItems =
                        _bakeryDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                            .Include(s => s.Product)
                            .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _bakeryDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bakeryDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _bakeryDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            // Getting total using LINQ
            var total = _bakeryDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }

    }
}