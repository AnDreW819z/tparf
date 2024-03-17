using tparf.api.Entities;
using tparf.dto.Orders;

namespace tparf.api.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateNewOrder(CreateOrderDto orderDto);
        public Task<decimal> AddOrderItem(List<CartItem> cartItems, long orderId);
        Task<Order> UpdateStatus(long id, int status);
        Task<Order> DeleteOrder(long id);
        Task<List<Order>> GetOrderByUser(long cartId);
        Task<List<OrderItem>> GetOrderItems(long orderId);
        Task<List<Order>> GetOrders();
        Task<List<OrderStatus>> GetStatuses();
        Task<Order> GetOrder(long id);
    }
}
