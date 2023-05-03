using tparf.Models;

namespace tparf.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(Guid orderId);
        ICollection<Order> GetOrdersOfAUser(Guid userId);
        ICollection<Order> GetOrdersOfAProduct(Guid productId);
        bool OrderExists(Guid orderId);
        bool CreateOrder(Guid userId, Guid productId,Order order);
        bool Save();
    }
}
