using Microsoft.AspNetCore.Mvc;
using tparf.api.Extensions;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Categories;
using tparf.dto.Product;
using tparf.dto.Subcategories;

namespace tparf.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        public SubcategoryController(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        [HttpGet]
        [Route("getSubcategories")]
        public async Task<ActionResult<List<SubcategoryDto>>> GetCategories()
        {
            try
            {
                var subcategories = await _subcategoryRepository.GetSubcategories();
                if (subcategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var subcategoriesDto = subcategories.ConvertToDto();
                    return Ok(subcategoriesDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getSubcategory/{id:long}")]
        public async Task<ActionResult<SubcategoryDto>> GetSubcategory(long id)
        {
            try
            {
                var subcategory = await _subcategoryRepository.GetSubcategory(id);
                if (subcategory == null)
                {
                    return NotFound();
                }
                else
                {
                    var subcategoryDto = subcategory.ConverToDto();
                    return Ok(subcategoryDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("getProductsFromSubcategory/{id:long}")]
        public async Task<ActionResult<SubcategoryDto>> GetProductsFromSubcategory(long id)
        {
            try
            {
                var products = await _subcategoryRepository.GetProductFromSubcategory(id);
                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productsDto = products.ConvertToDto();
                    return Ok(productsDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("{subId:long}/getProductsFromSubcategoryWithManufacturer/{manId:long}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsFromSubcategoryWithManufacturer(long subId, long manId)
        {
            try
            {

                var products = await _subcategoryRepository.GetProductFromSubcategoryWithManufacturer(subId, manId);
                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = products.ConvertToDto();
                    return Ok(productDto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet]
        [Route("{manId:long}/getSubcategoriesFromManufacturer")]
        public async Task<ActionResult<List<SubcategoryDto>>> GetSubcategoriesFromManufacturer(long manId)
        {
            try
            {
                var subcategories = await _subcategoryRepository.GetSubcategoriesFromManufacturer(manId);
                var subDto = subcategories.ConvertToDto();
                return Ok(subDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewSubcategory([FromBody] CreateSubcategoryDto subcategoryDto)
        {
            try
            {
                var newSubcategory = await _subcategoryRepository.AddNewSubcategory(subcategoryDto);
                if (newSubcategory == null)
                {
                    return NoContent();
                }
                return Ok(newSubcategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubcategory(long id, UpdateSubcategoryDto subcategoryDto)
        {
            try
            {
                var updateSubategory = await _subcategoryRepository.UpdateSubcategory(id, subcategoryDto);
                if (updateSubategory == null)
                {
                    return NoContent();
                }
                var response = await _subcategoryRepository.GetSubcategory(updateSubategory.Id);
                var result = response.ConverToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Status>> DeleteCategory(long id)
        {
            try
            {
                var subcategory = await _subcategoryRepository.DeleteSubcategory(id);
                return subcategory;
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }
    }
}
