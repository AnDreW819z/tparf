using tparf.Models;

namespace tparf.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(Guid productId);
        ICollection<Product> GetProductByCategory(Guid categoryId);
        ICollection<Product> GetProductByManufacturer(Guid manufacturerId);
        ICollection<User> GetUserByProduct(Guid productId);
        ICollection<ProductProperty> GetProductPropertyByProduct(Guid productId);
        bool ProductExists(Guid productId);
        bool CreateProduct(Guid manufacturerId, Guid categoryId, Product product);
        bool Save();
    }
}
