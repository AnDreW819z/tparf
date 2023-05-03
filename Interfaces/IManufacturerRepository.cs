using tparf.Models;

namespace tparf.Interfaces
{
    public interface IManufacturerRepository
    {
        ICollection<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturer(Guid manufacturerId);
        Manufacturer GetManufacturerByProduct(Guid productId);
        ICollection<Product> GetProductByManufacturer(Guid manufacturerId);
        bool ManufacturerExists(Guid manufacturerId);
        bool CreateManufacturer(Manufacturer category);
        bool Save();
    }
}
