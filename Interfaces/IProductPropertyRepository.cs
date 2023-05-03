using tparf.Models;

namespace tparf.Interfaces
{
    public interface IProductPropertyRepository
    {
        bool ProductPropertyExists(Guid productPropertyId);
        bool CreateProductProperty(Guid productId,ProductProperty productProperty);
        bool Save();
    }
}
