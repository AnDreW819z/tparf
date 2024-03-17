using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Orders;

namespace tparf.api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TparfDbContext _tparfDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderRepository(TparfDbContext tparfDbContext, UserManager<ApplicationUser> userManager)
        {
            _tparfDbContext = tparfDbContext;
            _userManager = userManager;
        }

        public async Task<bool> CartExist(long cartId)
        {
            return await _tparfDbContext.Carts.AnyAsync(c => c.Id == cartId);
        }

        public async Task<decimal> AddOrderItem(List<CartItem> cartItems, long orderId)
        {

            if (cartItems != null)
            {
                decimal totalprice = 0;
                foreach (var item in cartItems)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        ProductName = await _tparfDbContext.Products.Where(p => p.Id == item.ProductId).Select(p => p.Name).FirstOrDefaultAsync(),
                        Price = await _tparfDbContext.Products.Where(p => p.Id == item.ProductId).Select(p => p.Price).FirstOrDefaultAsync(),
                        Qty = item.Qty
                    };
                    orderItem.TotalPriceByOrderItem = orderItem.Price * orderItem.Qty;
                    if (orderItem != null)
                    {
                        await _tparfDbContext.OrderItems.AddAsync(orderItem);
                        _tparfDbContext.CartItems.Remove(item);
                        totalprice += orderItem.TotalPriceByOrderItem;
                    }


                }
                await _tparfDbContext.SaveChangesAsync();
                return totalprice;
            }
            return 0;
        }

        public async Task<Order> CreateNewOrder(CreateOrderDto orderDto)
        {
            if (await CartExist(orderDto.CartId))
            {
                var userId = await _tparfDbContext.Carts.FindAsync(orderDto.CartId);
                var user = await _userManager.FindByIdAsync(userId.UserId.ToString());
                Order order = new Order
                {
                    CartId = orderDto.CartId,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TotalPrice = orderDto.TotalPrice,
                    Adress = orderDto.Adress,
                    StatusId = 1,
                    CreateTime = DateTime.UtcNow
                };
                if (order != null)
                {
                    var cartItems = await _tparfDbContext.CartItems.Where(c => c.CartId == orderDto.CartId).ToListAsync();
                    await _tparfDbContext.Orders.AddAsync(order);
                    //order.TotalPrice = await AddOrderItem(cartItems, order.Id);
                    var result = await _tparfDbContext.Orders.AddAsync(order);
                    await _tparfDbContext.SaveChangesAsync();
                    order.TotalPrice = await AddOrderItem(cartItems, order.Id);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return default;
        }

        public async Task<Order> DeleteOrder(long id)
        {
            var order = await _tparfDbContext.Orders.FindAsync(id);
            var orderItems = await GetOrderItems(id);
            if (order != null && orderItems != null)
            {
                foreach (var item in orderItems)
                {
                    _tparfDbContext.OrderItems.Remove(item);
                }
                _tparfDbContext.Orders.Remove(order);
                _tparfDbContext.SaveChanges();
                return order;
            }
            return default;
        }

        public async Task<List<Order>> GetOrderByUser(long cartId)
        {
            var orders = await _tparfDbContext.Orders.Where(o => o.CartId == cartId).ToListAsync();
            return orders;
        }

        public async Task<List<OrderItem>> GetOrderItems(long orderId)
        {
            var orderItems = await _tparfDbContext.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
            return orderItems;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _tparfDbContext.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> UpdateStatus(long id, int statusId)
        {
            var order = await _tparfDbContext.Orders.FindAsync(id);
            if (order != null)
            {
                order.StatusId = statusId;
                await _tparfDbContext.SaveChangesAsync();
                return order;
            }
            return default;
        }

        public async Task<List<OrderStatus>> GetStatuses()
        {
            var statuses = await _tparfDbContext.OrderStatuses.ToListAsync();
            if (statuses != null)
            {
                return statuses;
            }
            return default;
        }

        public async Task<OrderStatus> GetStatusById(int id)
        {
            var status = await _tparfDbContext.OrderStatuses.FindAsync(id);
            if (status != null)
            {
                return status;
            }
            return default;
        }

        public async Task<Order> GetOrder(long id)
        {
            var order = await _tparfDbContext.Orders.FindAsync(id);
            if (order != null)
            {
                return order;
            }
            return default;

        }
    }
}
