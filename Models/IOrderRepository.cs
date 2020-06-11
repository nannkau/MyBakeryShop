using System.Collections.Generic;

namespace MyBakeryShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        IEnumerable<OrderDetail> ListProduct(int? id);
    }
}