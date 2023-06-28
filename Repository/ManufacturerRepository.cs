using AutoMapper;
using System.Diagnostics.Metrics;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;
namespace tparf.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ManufacturerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateManufacturer(Manufacturer category)
        {
            _context.Add(category);
            return Save();
        }

        public Manufacturer GetManufacturer(int manufacturerId)
        {
            return _context.Manufacturers.Where(c => c.Id == manufacturerId).FirstOrDefault();
        }

        public Manufacturer GetManufacturerByProduct(int productId)
        {
            return _context.Products.Where(o => o.Id == productId).Select(c => c.Manufacturer).FirstOrDefault();
        }

        public ICollection<Manufacturer> GetManufacturers()
        {
            return _context.Manufacturers.ToList();
        }

        public ICollection<Product> GetProductByManufacturer(int manufacturerId)
        {
            return _context.Products.Where(c => c.Manufacturer.Id == manufacturerId).ToList();
        }

        public bool ManufacturerExists(int manufacturerId)
        {
            return _context.Manufacturers.Any(c => c.Id == manufacturerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
