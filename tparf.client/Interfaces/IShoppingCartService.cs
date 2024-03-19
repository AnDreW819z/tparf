using tparf.dto.CartItems;

namespace tparf.client.Interfaces
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(long userId);
        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItemDto> DeleteItem(long id);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);
    }
}
