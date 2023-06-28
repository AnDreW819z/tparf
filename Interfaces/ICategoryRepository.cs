using System.Diagnostics.Metrics;
using tparf.Models;

namespace tparf.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);
        bool CategoryExists(int categoryId);
        bool CreateCategory(Category category);
        bool Save();
    }
}
