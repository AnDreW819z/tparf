using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateOrder(Guid userId, Guid productId, Order order)
        {
            _context.Add(order);
            return Save();
        }

        public Order GetOrder(Guid orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public ICollection<Order> GetOrdersOfAProduct(Guid productId)
        {
            return _context.Orders.Where(c => c.Product.Id == productId).ToList();
        }

        public ICollection<Order> GetOrdersOfAUser(Guid userId)
        {
            return _context.Orders.Where(c => c.User.Id == userId).ToList();
        }

        public bool OrderExists(Guid orderId)
        {
            return _context.Orders.Any(c => c.Id == orderId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
