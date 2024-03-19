using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Subcategories;

namespace tparf.api.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {

        private readonly TparfDbContext _tparfDbContext;
        public SubcategoryRepository(TparfDbContext tparfDbContext, ICategoryRepository categoryRepository)
        {
            _tparfDbContext = tparfDbContext;
        }

        private async Task<bool> SubcategoryExist(long subId)
        {
            return await _tparfDbContext.Subcategories.AnyAsync(c => c.Id == subId);
        }
        public async Task<Subcategory> AddNewSubcategory(CreateSubcategoryDto createSubDto)
        {
            if (await SubcategoryExist(createSubDto.Id) == false)
            {
                Subcategory subcategory = new Subcategory
                {
                    Name = createSubDto.Name,
                    IconCss = createSubDto.IconCss,
                    ImageUrl = createSubDto.ImageUrl,
                    CategoryId = createSubDto.CategoryId

                };
                if (subcategory != null)
                {
                    var result = await _tparfDbContext.Subcategories.AddAsync(subcategory);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public async Task<Status> DeleteSubcategory(long id)
        {
            var subcategory = await _tparfDbContext.Subcategories.FindAsync(id);
            if (subcategory != null)
            {
                _tparfDbContext.Subcategories.Remove(subcategory);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Категория успешно удаленa", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<List<Product>> GetProductFromSubcategory(long id)
        {
            var products = await _tparfDbContext.Products.Include(p => p.Subcategory).Include(p=>p.Manufacturer).Where(p => p.SubcategoryId == id).ToListAsync();
            return products;
        }

        public async Task<List<Subcategory>> GetSubcategories()
        {
            var subcategories = await _tparfDbContext.Subcategories.Include(s => s.Category).ToListAsync();
            return subcategories;
        }

        public async Task<Subcategory> GetSubcategory(long id)
        {
            if (await SubcategoryExist(id))
            {
                var subcategory = await _tparfDbContext.Subcategories.FindAsync(id);
                subcategory.Category = await _tparfDbContext.Categories.FindAsync(subcategory.CategoryId);
                return subcategory;
            }
            return null;
        }

        public async Task<List<Subcategory>> GetSubcategoriesFromManufacturer(long id)
        {
            var products = await _tparfDbContext.Products.Where(c => c.ManufacturerId == id).Include(s => s.Manufacturer).ToListAsync();
            List<Subcategory> subcategories = new List<Subcategory>();
            foreach (var product in products)
            {
                subcategories.Add(await GetSubcategory(product.SubcategoryId));   
            }
            if (subcategories != null)
            {
                var result = subcategories.GroupBy(s => s.Id).Select(s => s.FirstOrDefault()).ToList();
                return result;
            }
                
            return default;
        }

        public async Task<Subcategory> UpdateSubcategory(long id, UpdateSubcategoryDto updateCatDto)
        {
            var subcategory = await _tparfDbContext.Subcategories.FindAsync(id);
            if (subcategory != null)
            {
                subcategory.Name = updateCatDto.Name;
                subcategory.IconCss = updateCatDto.IconCss;
                subcategory.ImageUrl = updateCatDto.ImageUrl;
                subcategory.CategoryId = updateCatDto.CategoryId;
                await _tparfDbContext.SaveChangesAsync();
                return subcategory;
            }
            return null;
        }

        public async Task<List<Product>> GetProductFromSubcategoryWithManufacturer(long subId, long manId)
        {
            var products = await _tparfDbContext.Products.Include(p => p.Subcategory).Include(p => p.Manufacturer).Where(p => p.SubcategoryId == subId && p.ManufacturerId == manId).ToListAsync();
            return products;
        }
    }
}
