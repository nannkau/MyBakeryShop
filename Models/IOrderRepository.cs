namespace MyBakeryShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}