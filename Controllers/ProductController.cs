using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tparf.Dto;
using tparf.Interfaces;
using tparf.Models;
using tparf.Repository;

namespace tparf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductController(IProductRepository productRepository,
            ICategoryRepository categoryRepository, IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        [HttpGet("{productId}/productProperty")]
        [ProducesResponseType(200, Type = typeof(ProductProperty))]
        [ProducesResponseType(400)]
        public IActionResult GetProductPropertyByProduct(Guid productId)
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
        public IActionResult GetProduct(Guid productId)
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
        public IActionResult CreateProduct([FromQuery] Guid manufacturerId, [FromQuery] Guid categoryId, [FromBody] ProductDto productCreate)
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
            productMap.Category = _categoryRepository.GetCategory(categoryId);

            if (!_productRepository.CreateProduct(manufacturerId, categoryId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}
