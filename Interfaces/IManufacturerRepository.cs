using tparf.Models;

namespace tparf.Interfaces
{
    public interface IManufacturerRepository
    {
        ICollection<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturer(int manufacturerId);
        Manufacturer GetManufacturerByProduct(int productId);
        ICollection<Product> GetProductByManufacturer(int manufacturerId);
        bool ManufacturerExists(int manufacturerId);
        bool CreateManufacturer(Manufacturer category);
        bool Save();
    }
}
