using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SubcategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool SubcategoryExists(int subcategoryId)
        {
            return _context.Categories.Any(c => c.Id == subcategoryId);
        }

        public bool CreateSubcategory(int categoryId, Subcategory subcategory)
        {
            _context.Add(subcategory);
            return Save();
        }

        public ICollection<Subcategory> GetSubcategories()
        {
            return _context.Subcategories.ToList();
        }

        public Subcategory GetSubcategory(int subcategoryId)
        {
            return _context.Subcategories.Where(c => c.Id == subcategoryId).FirstOrDefault();
        }

        public Subcategory GetSubcategoryByProduct(int productId)
        {
            return _context.Products.Where(o => o.Id == productId).Select(c => c.Subcategory).FirstOrDefault();
        }

        public ICollection<Product> GetProductBySubcategories(int subcategoryId)
        {
            return _context.Products.Where(c => c.Subcategory.Id == subcategoryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
