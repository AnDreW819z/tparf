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
    public class ProductPropertyController : Controller
    {
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductPropertyController(IProductPropertyRepository productPropertyRepository, 
            IProductRepository productRepository, 
            IMapper mapper)
        {
            _productPropertyRepository = productPropertyRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductProperty([FromQuery] Guid productId,  [FromBody] ProductPropertyDto productPropertyCreate)
        {
            if (productPropertyCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productPropertyMap = _mapper.Map<ProductProperty>(productPropertyCreate);

            productPropertyMap.Product = _productRepository.GetProduct(productId);

            if (!_productPropertyRepository.CreateProductProperty(productId, productPropertyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}
