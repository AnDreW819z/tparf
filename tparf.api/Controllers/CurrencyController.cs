using Microsoft.AspNetCore.Mvc;
using tparf.api.Interfaces;
using tparf.api.Repository;
using tparf.dto.Auth;
using tparf.dto.Currensies;
using tparf.dto.Manufacturer;

namespace tparf.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currency;
        public CurrencyController(ICurrencyRepository currency)
        {
            _currency = currency;
        }

        [HttpGet]
        [Route("getCurrensies")]
        public async Task<ActionResult<List<СurrenciesDto>>> GetCurrencies()
        {
            try
            {
                var currencies = await _currency.GetCurrencies();
                if (currencies == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(currencies);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddNewCurrency([FromBody] СurrenciesDto manufacturerDto)
        {
            try
            {
                var newManufacturer = await _currency.AddNewCurrency(manufacturerDto);
                if (newManufacturer == null)
                {
                    return NoContent();
                }
                return Ok(newManufacturer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrency(int id, UpdateCurrenciesDto manufacturerDto)
        {
            try
            {
                var updateManufacturer = await _currency.UpdateCurrency(id, manufacturerDto);
                if (updateManufacturer == null)
                {
                    return NoContent();
                }
                return Ok("Успешно изменено");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteCurrency(int id)
        {
            try
            {
                var manufacturer = await _currency.DeleteCurrency(id);
                return manufacturer;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
