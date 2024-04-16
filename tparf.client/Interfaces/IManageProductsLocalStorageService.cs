using tparf.dto.Product;

namespace tparf.client.Interfaces
{
	public interface IManageProductsLocalStorageService
	{
		//Task<List<ProductDto>> GetCollection();
		Task RemoveCollection();
	}
}
