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

        private async Task<bool> CategoryExist(long? catId)
        {
            return await _tparfDbContext.Categories.AnyAsync(c => c.Id == catId);
        }

        public async Task<Category> AddNewCategory(CreateCategoryDto createCatDto)
        {
            if (await CategoryExist(createCatDto.Id) == false)
            {
                if (createCatDto.ParentId != 0 && await CategoryExist(createCatDto.ParentId) == false)
                    return null;
                Category category = new Category
                {
                    Name = createCatDto.Name,
                    IconCss = createCatDto.IconCss,
                    ImageUrl = createCatDto.ImageUrl,
                    ParentId = createCatDto.ParentId,
                };
                if (createCatDto.ParentId == 0)
                    category.ParentId = null;
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
            var categoriesParent = categories.Where(c=>c.ParentId == null).ToList();
            List<Category> result = new List<Category>();
            foreach (var category in categoriesParent)
            {
                var response = await GetCategory(category.Id);
                result.Add(response);
            }
            return result;
        }

        public async Task<Category> GetCategory(long id)
        {
            if (await CategoryExist(id))
            {
                var category = await _tparfDbContext.Categories.FindAsync(id);
                category.Children = await _tparfDbContext.Categories.Where(c=> c.ParentId == category.Id).ToListAsync();
                return category;
            }
            return null;
        }

        public async Task<List<Product>> GetProductFromCategory(long id)
        {
            var products = await _tparfDbContext.Products.Include(p => p.Category).Include(p => p.Manufacturer).Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }

        public async Task<List<Category>> GetCategoriesFromManufacturer(long id)
        {
            var products = await _tparfDbContext.Products.Where(c => c.ManufacturerId == id).Include(s => s.Manufacturer).ToListAsync();
            List<Category> subcategories = new List<Category>();
            foreach (var product in products)
            {
                subcategories.Add(await GetCategory(product.CategoryId));
            }
            if (subcategories != null)
            {
                var result = subcategories.GroupBy(s => s.Id).Select(s => s.FirstOrDefault()).ToList();
                return result;
            }

            return default;
        }

        public async Task<List<Product>> GetProductFromCategoryWithManufacturer(long subId, long manId)
        {
            var products = await _tparfDbContext.Products.Include(p => p.Category).Include(p => p.Manufacturer).Where(p => p.CategoryId == subId && p.ManufacturerId == manId).ToListAsync();
            return products;
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
