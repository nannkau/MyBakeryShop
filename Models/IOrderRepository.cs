using System.Collections.Generic;

namespace MyBakeryShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        List<OderItem> ListProduct(int? id);
    }
}