using AutoMapper;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateProduct(Guid manufacturerId, Guid categoryId, Product product)
        {
            _context.Add(product);
            return Save();
        }

        public Product GetProduct(Guid productId)
        {
            return _context.Products.Where(o => o.Id == productId).FirstOrDefault();
        }

        public ICollection<Product> GetProductByManufacturer(Guid manufacturerId)
        {
            return _context.Products.Where(p => p.Manufacturer.Id == manufacturerId).ToList();
        }

        public ICollection<Product> GetProductByCategory(Guid categoryId)
        {
            return _context.Products.Where(p => p.Category.Id == categoryId).ToList();
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public ICollection<User> GetUserByProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public bool ProductExists(Guid productId)
        {
            return _context.Products.Any(o => o.Id == productId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<ProductProperty> GetProductPropertyByProduct(Guid productId)
        {
            return _context.ProductProperties.Where(p => p.Product.Id == productId).ToList();
        }
    }
}
