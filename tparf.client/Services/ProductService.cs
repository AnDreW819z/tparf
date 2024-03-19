using System.Net.Http.Json;
using tparf.client.Interfaces;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Images;
using tparf.dto.Subcategories;

namespace tparf.client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDtos> GetProduct(long id)
        {
            try
            {
                var responce = await _httpClient.GetAsync($"api/Product/getProduct/{id}");
                if(responce.IsSuccessStatusCode)
                {
                    if (responce.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDtos);
                    }
                    return await responce.Content.ReadFromJsonAsync<ProductDtos>();
                }
                
                else
                {
                    var message = await responce.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CharacteristicDto>> GetCharacteristicById(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}/getCharacteristics");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await response.Content.ReadFromJsonAsync<List<CharacteristicDto>>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Product/getProducts");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ProductDto>> GetProductsFromSubcategory(long subId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Subcategory/{subId}/getProducts");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<List<ProductDto>> GetProductsFromSubcategoryWithManufacturer(long subId, long manId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Subcategory/{subId}/getProductsFromSubcategoryWithManufacturer/{manId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<ManufacturerDto>> GetManufacturers()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Manufacturer/getManufacturers");
                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<ManufacturerDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesFromManufacturer(long manId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Manufacturer/{manId}/getCategories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<SubcategoryDto>> GetSubcategoriesFromCategory(long catId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Category/{catId}/getSubcategories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<SubcategoryDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<SubcategoryDto>> GetSubcategoriesFromManufacturer(long manId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Subcategory/{manId}/getSubcategoriesFromManufacturer");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<List<SubcategoryDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<CategoryDto>> GetCategories()
		{
			try
			{
				var response = await _httpClient.GetAsync("api/Category/getCategories");
				if (response.IsSuccessStatusCode)
				{
					if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
					{
						return default;
					}
					return await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
				}
				else
				{
					var message = await response.Content.ReadAsStringAsync();
					throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
				}
			}
			catch
			{
				throw;
			}
		}

        public async Task<List<SubcategoryDto>> GetSubcategories()
		{
			try
			{
				var response = await _httpClient.GetAsync("api/Subcategory/getSubcategories");
				if (response.IsSuccessStatusCode)
				{
					if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
					{
						return default;
					}
					return await response.Content.ReadFromJsonAsync<List<SubcategoryDto>>();
				}
				else
				{
					var message = await response.Content.ReadAsStringAsync();
					throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
				}
			}
			catch
			{
				throw;
			}
		}

        public async Task<List<ImageDto>> GetImagesById(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}/getImages");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await response.Content.ReadFromJsonAsync<List<ImageDto>>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
