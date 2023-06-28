using tparf.Models;

namespace tparf.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int productId);
        ICollection<Product> GetProductByCategory(int categoryId);
        ICollection<Product> GetProductByManufacturer(int manufacturerId);
        //ICollection<ApplicationUser> GetUserByProduct(int productId);
        ICollection<ProductProperty> GetProductPropertyByProduct(int productId);
        bool ProductExists(int productId);
        bool CreateProduct(int manufacturerId, int subcategoryId, Product product);
        bool Save();
    }
}
