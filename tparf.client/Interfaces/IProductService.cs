using tparf.dto.Categories;
using tparf.dto.Manufacturer;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Images;
using tparf.dto.Subcategories;

namespace tparf.client.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDtos> GetProduct(long id);
        public Task<CategoryDto> GetCategory(long id);
        public Task<List<ProductDto>> GetProducts();
        public Task<List<ProductDto>> GetProductsFromSubcategory(long subId);
        public Task<List<ProductDto>> GetProductsFromSubcategoryWithManufacturer(long subId, long manId);
        public Task<List<ManufacturerDto>> GetManufacturers();
        public Task<List<CategoryDto>> GetCategoriesFromManufacturer(long manId);
        public Task<List<SubcategoryDto>> GetSubcategoriesFromCategory(long catId);
        public Task<List<SubcategoryDto>> GetSubcategoriesFromManufacturer(long manId);
        public Task<List<SubcategoryDto>> GetSubcategories();
        public Task<List<CategoryDto>> GetCategories();
        public Task<List<CharacteristicDto>> GetCharacteristicById(long id);
        public Task<List<ImageDto>> GetImagesById(long id);


    }
}
