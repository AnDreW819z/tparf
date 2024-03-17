using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Subcategories;

namespace tparf.api.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> GetSubcategories();
        Task<Subcategory> GetSubcategory(long id);
        Task<List<Subcategory>> GetSubcategoriesFromManufacturer(long id);
        Task<List<Product>> GetProductFromSubcategory(long id);
        Task<List<Product>> GetProductFromSubcategoryWithManufacturer(long subId, long manId);

        public Task<Subcategory> AddNewSubcategory(CreateSubcategoryDto createCatDto);
        public Task<Subcategory> UpdateSubcategory(long id, UpdateSubcategoryDto updateCatDto);
        public Task<Status> DeleteSubcategory(long id);
    }
}
