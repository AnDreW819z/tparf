using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using tparf.Dto;
using tparf.Interfaces;
using tparf.Models;

namespace tparf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerRepository manufacturerRepository, IMapper mapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Manufacturer>))]
        public IActionResult GetManufacturers()
        {
            var manufacturers = _mapper.Map<List<ManufacturerDto>>(_manufacturerRepository.GetManufacturers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(manufacturers);
        }

        [HttpGet("{manufacturerId}")]
        [ProducesResponseType(200, Type = typeof(Manufacturer))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(Guid manufacturerId)
        {
            if (!_manufacturerRepository.ManufacturerExists(manufacturerId))
                return NotFound();
            var manufacturer = _mapper.Map<ManufacturerDto>(_manufacturerRepository.GetManufacturer(manufacturerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(manufacturer);
        }

        [HttpGet("/manufacturer/{productId}")]
        [ProducesResponseType(200, Type = typeof(Manufacturer))]
        [ProducesResponseType(400)]
        public IActionResult GetManufacturerByProduct(Guid productId)
        {
            var manufacturer = _mapper.Map<ManufacturerDto>(
                _manufacturerRepository.GetManufacturerByProduct(productId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(manufacturer);

        }

        [HttpGet("{manufacturerId}/product")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProductByManufacturer(Guid manufacturerId)
        {
            if (!_manufacturerRepository.ManufacturerExists(manufacturerId))
            {
                return NotFound();
            }
            var product = _mapper.Map<List<ProductDto>>(_manufacturerRepository.GetProductByManufacturer(manufacturerId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateManufacturer([FromBody] ManufacturerDto manufacturerCreate)
        {
            if (manufacturerCreate == null)
                return BadRequest(ModelState);

            var manufacturer = _manufacturerRepository.GetManufacturers()
                .Where(c => c.Name.Trim().ToUpper() == manufacturerCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (manufacturer != null)
            {
                ModelState.AddModelError("", "Поставщик уже существует");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var manufacturerMap = _mapper.Map<Manufacturer>(manufacturerCreate);

            if (!_manufacturerRepository.CreateManufacturer(manufacturerMap))
            {
                ModelState.AddModelError("", "Ошибка при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}
