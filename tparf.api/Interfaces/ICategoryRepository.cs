using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Categories;

namespace tparf.api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(long id);
        Task<List<Subcategory>> GetSubcategoryFromCategory(long catid);
        //Task<IEnumerable<TpaProduct>> GetProductFromCategory(long id);

        public Task<Category> AddNewCategory(CategoryDto createCatDto);
        public Task<Category> UpdateCategory(long id, UpdateCategoryDto updateCatDto);
        public Task<Status> DeleteCategory(long id);
    }
}
