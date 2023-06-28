using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tparf.Data;
using tparf.Dto;
using tparf.Dto.AppUser.OtherObjects;
using tparf.Interfaces;
using tparf.Models;
using tparf.Repository;

namespace tparf.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductController(IProductRepository productRepository,
            ISubcategoryRepository subcategoryRepository, IManufacturerRepository manufacturerRepository, IMapper mapper )
        {
            _productRepository = productRepository;
            _subcategoryRepository = subcategoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        [HttpGet("{productId}/productProperty")]
        [ProducesResponseType(200, Type = typeof(ProductProperty))]
        [ProducesResponseType(400)]
        public IActionResult GetProductPropertyByProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            var productProperty = _mapper.Map<List<ProductPropertyDto>>(_productRepository.GetProductPropertyByProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(productProperty);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();
            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public IActionResult CreateProduct([FromQuery] int manufacturerId, int subcategoryId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var products = _productRepository.GetProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.Trim().ToUpper())
                .FirstOrDefault();
            

            if (products != null)
            {
                ModelState.AddModelError("", "Товар уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            productMap.Manufacturer = _manufacturerRepository.GetManufacturer(manufacturerId);
            productMap.Subcategory = _subcategoryRepository.GetSubcategory(subcategoryId);

            if (!_productRepository.CreateProduct(manufacturerId, subcategoryId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}
