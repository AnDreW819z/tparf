using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Categories;

namespace tparf.api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(long id);
        Task<List<Product>> GetProductFromCategory(long catid);
        Task<List<Category>> GetCategoriesFromManufacturer(long id);
        Task<List<Product>> GetProductFromCategoryWithManufacturer(long subId, long manId);
        public Task<Category> AddNewCategory(CreateCategoryDto createCatDto);
        public Task<Category> UpdateCategory(long id, UpdateCategoryDto updateCatDto);
        public Task<Status> DeleteCategory(long id);
    }
}
