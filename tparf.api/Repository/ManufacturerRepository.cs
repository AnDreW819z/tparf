using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Manufacturer;

namespace tparf.api.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly TparfDbContext _tparfDbContext;
        public ManufacturerRepository(TparfDbContext tparfDbContext)
        {
            _tparfDbContext = tparfDbContext;
        }

        private async Task<bool> ManufacturerExist(long manufacturerId)
        {
            return await _tparfDbContext.Manufacturers.AnyAsync(c => c.Id == manufacturerId);
        }

        public async Task<Manufacturer> AddNewManufacturer(ManufacturerDto manufacturerDto)
        {
            if (await ManufacturerExist(manufacturerDto.Id) == false)
            {
                Manufacturer manufacturer = new Manufacturer
                {
                    Name = manufacturerDto.Name,
                    IconCss= manufacturerDto.IconCss,
                    ImageUrl = manufacturerDto.ImageUrl,
                };
                if (manufacturer != null)
                {
                    var result = await _tparfDbContext.Manufacturers.AddAsync(manufacturer);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }

            }
            return null;
        }

        public async Task<Status> DeleteManufacturer(long id)
        {
            var manufacturer = await _tparfDbContext.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _tparfDbContext.Manufacturers.Remove(manufacturer);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Производитель успешно удален", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<Manufacturer> GetManufacturer(long id)
        {
            if (await ManufacturerExist(id))
            {
                var manufacturer = await _tparfDbContext.Manufacturers.SingleOrDefaultAsync(m => m.Id == id);
                return manufacturer;
            }
            return null;
        }

        public async Task<List<Manufacturer>> GetManufacturers()
        {
            var manufacturers = await _tparfDbContext.Manufacturers.ToListAsync();
            return manufacturers;
        }

        public async Task<Manufacturer> UpdateManufacturer(long id, UpdateManufacturerDto manufacturerDto)
        {
            var manufacturer = await _tparfDbContext.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                manufacturer.Name = manufacturerDto.Name;
                manufacturer.ImageUrl = manufacturerDto.ImageUrl;
                await _tparfDbContext.SaveChangesAsync();
                return manufacturer;
            }
            return null;
        }
    }
}
