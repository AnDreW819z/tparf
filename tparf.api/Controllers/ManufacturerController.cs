using Microsoft.AspNetCore.Mvc;
using tparf.api.Extensions;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;

namespace tparf.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturerController(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpGet]
        [Route("getManufacturers")]
        public async Task<ActionResult<List<ManufacturerDto>>> GetManufacturers()
        {
            try
            {
                var manufacturers = await _manufacturerRepository.GetManufacturers();
                if (manufacturers == null)
                {
                    return NotFound();
                }
                else
                {
                    var manufacturerDto = manufacturers.ConvertToDto();
                    return Ok(manufacturerDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }

        }

        [HttpGet]
        [Route("getManufacturer/{id:long}")]
        public async Task<ActionResult<ManufacturerDto>> GetManufacturer(long id)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.GetManufacturer(id);
                if (manufacturer == null)
                {
                    return NotFound();
                }
                else
                {
                    var manufacturerDto = manufacturer.ConverToDto();
                    return Ok(manufacturerDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        //[HttpGet]
        //[Route("{manufactuerId:long}/getCategories")]
        //public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoryFromManufacturer(long manufactuerId)
        //{
        //    try
        //    {
        //        var categories = await _manufacturerRepository.GetCategoryFromManufacturer(manufactuerId);
        //        var categoriesDto = categories.ConvertToDto();
        //        return Ok(categoriesDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ошибка получения данных из базы данных");
        //    }
        //}

        //[HttpGet]
        //[Route("getProducts/{manufacturerId:long}")]
        //public async Task<ActionResult<IEnumerable<TpaProductDto>>> GetProductsFromManufacturer(long manufacturerId)
        //{
        //    try
        //    {
        //        var products = await _manufacturerRepository.GetProductFromManufacturer(manufacturerId);
        //        var productsDto = products.ConvertToDto();
        //        return Ok(productsDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Ошибка получения данных из базы данных");
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> AddNewManufacturer([FromBody] ManufacturerDto manufacturerDto)
        {
            try
            {
                var newManufacturer = await _manufacturerRepository.AddNewManufacturer(manufacturerDto);
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
        public async Task<IActionResult> UpdateManufacturer(long id, UpdateManufacturerDto manufacturerDto)
        {
            try
            {
                var updateManufacturer = await _manufacturerRepository.UpdateManufacturer(id, manufacturerDto);
                if (updateManufacturer == null)
                {
                    return NoContent();
                }
                var response = await _manufacturerRepository.GetManufacturer(updateManufacturer.Id);
                var result = response.ConverToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteManufacturer(long id)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.DeleteManufacturer(id);
                return manufacturer;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
