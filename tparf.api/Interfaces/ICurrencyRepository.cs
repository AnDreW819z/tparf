using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Currensies;
using tparf.dto.Manufacturer;

namespace tparf.api.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Сurrencies>> GetCurrencies();

        public Task<Сurrencies> AddNewCurrency(СurrenciesDto currenciesDto);
        public Task<Сurrencies> UpdateCurrency(int id, UpdateCurrenciesDto currenciesDto);
        public Task<Status> DeleteCurrency(int id);
    }
}
