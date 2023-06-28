using tparf.Models;

namespace tparf.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int orderId);
        //ICollection<Order> GetOrdersOfAUser(int userId);
        ICollection<Order> GetOrdersOfAProduct(int productId);
        bool OrderExists(int orderId);
        bool CreateOrder(/*int userId,*/ int productId,Order order);
        bool Save();
    }
}
