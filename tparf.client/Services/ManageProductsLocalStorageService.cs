using Blazored.LocalStorage;
using tparf.client.Interfaces;
using tparf.dto.Product;

namespace tparf.client.Services
{
	public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly IProductService _productService;
		private const string key = "productCollection";

		public ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService)
		{
			_localStorageService = localStorageService;
			_productService = productService;
		}

		//public async Task<List<ProductDto>> GetCollection()
		//{
		//	return await _localStorageService.GetItemAsync<List<ProductDto>>(key) ?? await AddCollection();
		//}

		public async Task RemoveCollection()
		{
			await _localStorageService.RemoveItemAsync(key);
		}
		//private async Task<List<ProductDto>> AddCollection()
		//{
		//	var productCollection = await _productService.GetProducts();
		//	if (productCollection != null)
		//	{
		//		await _localStorageService.SetItemAsync(key, productCollection);
		//	}
		//	return productCollection;
		//}
	}
}
