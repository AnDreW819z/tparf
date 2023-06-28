using tparf.Models;

namespace tparf.Interfaces
{
    public interface ISubcategoryRepository
    {
        ICollection<Subcategory> GetSubcategories();
        Subcategory GetSubcategory(int subcategoryId);
        Subcategory GetSubcategoryByProduct(int productId);
        ICollection<Product> GetProductBySubcategories(int subcategoryId);
        bool SubcategoryExists(int SubcategoryId);
        bool CreateSubcategory(int categoryId, Subcategory subcategory);
        bool Save();
    }
}
