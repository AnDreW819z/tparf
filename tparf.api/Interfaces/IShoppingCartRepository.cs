using tparf.api.Entities;
using tparf.dto.CartItems;

namespace tparf.api.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(long id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(long id);
        Task<CartItem> GetItem(long id);
        Task<List<CartItem>> GetItems(long userId);

    }
}
