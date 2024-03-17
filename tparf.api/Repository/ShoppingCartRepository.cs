using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.CartItems;

namespace tparf.api.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly TparfDbContext _tparfDbContext;

        public ShoppingCartRepository(TparfDbContext tparfDbContext)
        {
            _tparfDbContext = tparfDbContext;
        }

        private async Task<bool> CartItemExists(long cartId, long productId)
        {
            return await _tparfDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _tparfDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await _tparfDbContext.CartItems.AddAsync(item);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return default;

        }

        public async Task<CartItem> DeleteItem(long id)
        {
            var item = await _tparfDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                _tparfDbContext.CartItems.Remove(item);
                await _tparfDbContext.SaveChangesAsync();
            }

            return item;

        }

        public async Task<CartItem> GetItem(long id)
        {
            return await (from cart in _tparfDbContext.Carts
                          join cartItem in _tparfDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<List<CartItem>> GetItems(long userId)
        {
            return await (from cart in _tparfDbContext.Carts
                          join cartItem in _tparfDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(long id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _tparfDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await _tparfDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}
