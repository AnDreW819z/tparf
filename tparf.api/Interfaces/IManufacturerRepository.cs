using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Manufacturer;

namespace tparf.api.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetManufacturers();
        Task<Manufacturer> GetManufacturer(long id);
        //Task<IEnumerable<Category>> GetCategoryFromManufacturer(long id);
        //Task<IEnumerable<Subcategory>> GetSubcategoryFromManufacturer(long id);
        //Task<IEnumerable<TpaProduct>> GetProductFromManufacturer(long id);

        public Task<Manufacturer> AddNewManufacturer(ManufacturerDto manufacturerDto);
        public Task<Manufacturer> UpdateManufacturer(long id, UpdateManufacturerDto manufacturerDto);
        public Task<Status> DeleteManufacturer(long id);
    }
}
