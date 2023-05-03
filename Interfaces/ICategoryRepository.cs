using System.Diagnostics.Metrics;
using tparf.Models;

namespace tparf.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(Guid categoryId);
        Category GetCategoryByProduct(Guid productId);
        ICollection<Product> GetProductByCategories(Guid categoryId);
        bool CategoryExists(Guid categoryId);
        bool CreateCategory(Category category);
        bool Save();
    }
}
