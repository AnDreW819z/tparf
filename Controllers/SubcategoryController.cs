using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tparf.Dto;
using tparf.Dto.AppUser.OtherObjects;
using tparf.Interfaces;
using tparf.Models;
using tparf.Repository;

namespace tparf.Controllers
{
    [Route("api/subcategories")]
    [ApiController]
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SubcategoryController(ISubcategoryRepository subcategoryRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subcategory>))]
        public IActionResult GetSubcategories()
        {
            var subcategories = _mapper.Map<List<SubcategoryDto>>(_subcategoryRepository.GetSubcategories());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subcategories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Subcategory))]
        [ProducesResponseType(400)]
        public IActionResult GetSubcategory(int subcategoryId)
        {
            if (!_subcategoryRepository.SubcategoryExists(subcategoryId))
                return NotFound();
            var subcategory = _mapper.Map<SubcategoryDto>(_subcategoryRepository.GetSubcategory(subcategoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subcategory);
        }

        [HttpGet("{subcategoryId}/product")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProductBySubcategory(int subcategoryId)
        {
            if (!_subcategoryRepository.SubcategoryExists(subcategoryId))
            {
                return NotFound();
            }
            var productProperty = _mapper.Map<List<ProductDto>>(_subcategoryRepository.GetProductBySubcategories(subcategoryId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(productProperty);
        }

        [HttpGet("getsub/{productId}")]
        [ProducesResponseType(200, Type = typeof(Subcategory))]
        [ProducesResponseType(400)]
        public IActionResult GetSubcategoryByProduct(int productId)
        {
            var subcategory = _mapper.Map<SubcategoryDto>(
                _subcategoryRepository.GetSubcategoryByProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(subcategory);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public IActionResult CreateSubcategory([FromQuery] int categoryId, [FromBody] SubcategoryDto subcategoryCreate)
        {
            if (subcategoryCreate == null)
                return BadRequest(ModelState);

            var subcategory = _subcategoryRepository.GetSubcategories()
                .Where(c => c.Name.Trim().ToUpper() == subcategoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (subcategory != null)
            {
                ModelState.AddModelError("", "Подкатегория уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subcategoryMap = _mapper.Map<Subcategory>(subcategoryCreate);

            subcategoryMap.Category = _categoryRepository.GetCategory(categoryId);

            if (!_subcategoryRepository.CreateSubcategory(categoryId, subcategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}

