using tparf.dto.CartItems;

namespace tparf.client.Interfaces
{
	public interface IManageCartItemsLocalStorageService
	{
		Task<List<CartItemDto>> GetCollection();
		Task SaveCollection(List<CartItemDto> cartItemDtos);
		Task RemoveCollection();
	}
}
