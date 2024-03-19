using Blazored.LocalStorage;
using tparf.client.Interfaces;
using tparf.dto.CartItems;

namespace tparf.client.Services
{
	public class ManageCartItemLocalStorageService : IManageCartItemsLocalStorageService
    {
		private readonly ILocalStorageService _localStorageService;
		private readonly IShoppingCartService _shoppingCartService;
		const string key = "CartItemCollection";

		public ManageCartItemLocalStorageService(ILocalStorageService localStorageService,
												  IShoppingCartService shoppingCartService)
		{
			_localStorageService = localStorageService;
			_shoppingCartService = shoppingCartService;
		}

		public async Task<List<CartItemDto>> GetCollection()
		{
			return await _localStorageService.GetItemAsync<List<CartItemDto>>(key)
					?? await AddCollection();
		}

		public async Task SaveCollection(List<CartItemDto> cartItemDtos)
		{
			await _localStorageService.SetItemAsync(key, cartItemDtos);
		}

		private async Task<List<CartItemDto>> AddCollection()
		{
			var userId = await _localStorageService.GetItemAsync<long>("userId");
			Console.WriteLine($"userId = {userId}");
			var shoppingCartCollection = await _shoppingCartService.GetItems(userId);
			foreach (var shopCollection in shoppingCartCollection)
			{
				Console.WriteLine(shopCollection.ProductName);
			}

			if (shoppingCartCollection != null)
			{
				await _localStorageService.SetItemAsync(key, shoppingCartCollection);
				foreach(var collection in shoppingCartCollection)
				{
					Console.WriteLine(collection.ProductName);
				}
			}

			return shoppingCartCollection;

		}

		public async Task RemoveCollection()
		{
			await _localStorageService.RemoveItemAsync(key);
		}
	}
}
