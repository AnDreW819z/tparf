using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using tparf.Data;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Repository
{
    public class ProductPropertyRepository : IProductPropertyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductPropertyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateProductProperty(int productId, ProductProperty productProperty)
        {
            _context.Add(productProperty);
            return Save();
        }

        public bool ProductPropertyExists(int productPropertyId)
        {
            return _context.ProductProperties.Any(o => o.Id == productPropertyId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
