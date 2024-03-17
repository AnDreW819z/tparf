using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Categories;

namespace tparf.api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TparfDbContext _tparfDbContext;

        public CategoryRepository(TparfDbContext tparfDbContext)
        {
            _tparfDbContext = tparfDbContext;
        }

        private async Task<bool> CategoryExist(long catId)
        {
            return await _tparfDbContext.Categories.AnyAsync(c => c.Id == catId);
        }

        public async Task<Category> AddNewCategory(CategoryDto createCatDto)
        {
            if (await CategoryExist(createCatDto.Id) == false)
            {
                Category category = new Category
                {
                    Name = createCatDto.Name,
                    IconCss = createCatDto.IconCss,
                    ImageUrl = createCatDto.ImageUrl
                };
                if (category != null)
                {
                    var result = await _tparfDbContext.Categories.AddAsync(category);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<Status> DeleteCategory(long id)
        {
            var category = await _tparfDbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _tparfDbContext.Categories.Remove(category);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Категория успешно удаленa", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<List<Category>> GetCategories()
        {

            var categories = await _tparfDbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(long id)
        {
            if (await CategoryExist(id))
            {
                var category = await _tparfDbContext.Categories.FindAsync(id);
                return category;
            }
            return null;
        }

        //public async Task<IEnumerable<TpaProduct>> GetProductFromCategory(long id)
        //{
        //    var products = await _tparfDbContext.TpaProducts.Include(p=>p.Category).Where(p=>p.CategoryId== id).ToListAsync();
        //    return products;
        //}

        public async Task<List<Subcategory>> GetSubcategoryFromCategory(long catid)
        {
            var subcategory = await _tparfDbContext.Subcategories.Include(s => s.Category).Where(s => s.CategoryId == catid).ToListAsync();
            return subcategory;
        }

        public async Task<Category> UpdateCategory(long id, UpdateCategoryDto updateCatDto)
        {
            var category = await _tparfDbContext.Categories.FindAsync(id);
            if (category != null)
            {
                category.Name = updateCatDto.Name;
                category.IconCss = updateCatDto.IconCss;
                category.ImageUrl = updateCatDto.ImageUrl;
                await _tparfDbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
}
