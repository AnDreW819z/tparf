using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Currensies;
using tparf.dto.Manufacturer;

namespace tparf.api.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly TparfDbContext _tparfDbContext;

        public CurrencyRepository(TparfDbContext tparfDbContext)
        {
            _tparfDbContext = tparfDbContext;
        }
        private async Task<bool> CurrencyExist(string name)
        {
            return await _tparfDbContext.Сurrencies.AnyAsync(c => c.Name == name);
        }

        public async Task<Сurrencies> AddNewCurrency(СurrenciesDto currenciesDto)
        {
            if (await CurrencyExist(currenciesDto.Name) == false)
            {
                Сurrencies сurrency = new Сurrencies
                {
                    Name= currenciesDto.Name,
                    Value=currenciesDto.Value,
                    Symbol=currenciesDto.Symbol,
                };
                if (сurrency != null)
                {
                    var result = await _tparfDbContext.Сurrencies.AddAsync(сurrency);
                    await _tparfDbContext.SaveChangesAsync();
                    return result.Entity;
                }

            }
            return null;
        }

        public async Task<Status> DeleteCurrency(int id)
        {
            var currency = await _tparfDbContext.Сurrencies.FindAsync(id);
            if (currency != null)
            {
                _tparfDbContext.Сurrencies.Remove(currency);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Валюта успешно удалена", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<List<Сurrencies>> GetCurrencies()
        {
            var currency = await _tparfDbContext.Сurrencies.ToListAsync();
            return currency;
        }

        public async Task<Сurrencies> UpdateCurrency(int id, UpdateCurrenciesDto currenciesDto)
        {
            var currency = await _tparfDbContext.Сurrencies.FindAsync(id);
            if (currency != null)
            {
                currency.Name = currenciesDto.Name;
                currency.Value = currenciesDto.Value;
                currency.Symbol = currenciesDto.Symbol;
                await _tparfDbContext.SaveChangesAsync();
                return currency;
            }
            return null;
        }
    }
}
