using Microsoft.AspNetCore.Mvc;
using tparf.api.Entities;
using tparf.api.Extensions;
using tparf.api.Interfaces;
using tparf.api.ManufacturerSources;
using tparf.api.Repository;
using tparf.dto.Auth;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Descriptions;
using tparf.dto.Product.Images;

namespace tparf.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly Petropump _petropump;

        public ProductController(IProductRepository productRepository, Petropump petropump)
        {
            _productRepository = productRepository;
            _petropump = petropump;
        }

        [HttpGet]
        [Route("getProducts")]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetProducts();
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
        [Route("getProduct/{id:long}")]
        public async Task<ActionResult<ProductDtos>> GetProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var productDto = product.ConvertToDto();

                    return Ok(productDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");

            }
        }

        [HttpPost]
        [Route("addNewProduct")]
        public async Task<IActionResult> AddNewProduct([FromBody] CreateProductDto productDto)
        {
            try
            {

                var newProduct = await _productRepository.AddNewProduct(productDto);

                if (newProduct == null)
                {
                    return NoContent();
                }
                return Ok(productDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка создания");
            }
        }

        [HttpPost]
        [Route("addNewPetropumpProducts")]
        public async Task<ActionResult> AddNewPetropumpProduct()
        {
            try
            {
                //await _petropump.Download();
                await _petropump.AddProducts();
                return Ok("Готово");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка создания");
            }
        }


        [HttpPost]
        [Route("addNewProducts")]
        public async Task<IActionResult> AddNewProducts([FromBody] List<CreateProductDto> productDtos)
        {
            try
            {
                foreach(var productDto in productDtos)
                {
                    var newProduct = await _productRepository.AddNewProduct(productDto);

                    if (newProduct == null)
                    {
                        return NoContent();
                    }
                   
                }
                return Ok(productDtos);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("updateProduct/{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, UpdateProductDto productDto)
        {
            try
            {
                var product = await _productRepository.UpdateProduct(id, productDto);
                if (product == null)
                {
                    return NotFound();
                }

                var responce = await _productRepository.GetProduct(product.Id);
                var result = responce.ConvertToDto();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateProducts")]
        public async Task<IActionResult> UpdateProducts(List<CreateProductDto> productsDtos)
        {
            try
            {
                foreach (var productDto in productsDtos)
                {
                    UpdateProductDto uproductdto = new UpdateProductDto();
                    uproductdto.Name= productDto.Name;
                    uproductdto.Article = productDto.Article;
                    uproductdto.ImageUrl = productDto.ImageUrl;
                    uproductdto.ManufacturerId = productDto.ManufacturerId;
                    uproductdto.СategoryId = productDto.СategoryId;
                    uproductdto.Price = productDto.Price;
                    uproductdto.Discount = productDto.Discount;

                    var product = await _productRepository.UpdateProduct(productDto.Id, uproductdto);
                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                

                return Ok("Выполнено");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult<Status>> DeleteProduct(long id)
        {
            try
            {
                var category = await _productRepository.DeleteProduct(id);
                return category;
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }

        //[HttpDelete]
        //[Route("DeleteAllProducts")]
        //public async Task<ActionResult<Status>> DeleteAllProducts()
        //{
        //    try
        //    {
        //        var category = await _productRepository.DeleteAllProducts();
        //        return category;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Status { Message = ex.Message, StatusCode = 500 };
        //    }
        //}

        /////////// Characteristics //////////////

        [HttpGet]
        [Route("{prodId:long}/getCharacteristics")]
        public async Task<ActionResult<IEnumerable<CharacteristicDto>>> GetCharacteristicsFromProduct(long prodId)
        {
            try
            {
                var characteristics = await _productRepository.GetCharacteristicsFromProduct(prodId);
                var characteristicsDto = characteristics.ConvertToDto();
                return Ok(characteristicsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("characteristics/addNewCharacteristic")]
        public async Task<IActionResult> AddNewCharacteristic([FromBody] CharacteristicDto characteristic)
        {
            try
            {
                var newCharacteristic = await _productRepository.AddNewCharacteristic(characteristic);
                if (newCharacteristic == null)
                {
                    return NoContent();
                }
                return Ok(newCharacteristic);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPost]
        [Route("characteristics/addNewCharacteristics")]
        public async Task<IActionResult> AddNewCharacteristicss([FromBody] List<CharacteristicDto> characteristics)
        {
            try
            {
                foreach(var characteristic in characteristics)
                {
                    var newCharacteristic = await _productRepository.AddNewCharacteristic(characteristic);
                    if (newCharacteristic == null)
                    {
                        return NoContent();
                    }
                    
                }
                return Ok("Характеристики успешно загружены");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("characteristics/updateCharacteristics/{charId:long}")]
        public async Task<IActionResult> UpdateCharacteristic(long charId, UpdateCharacteristicDto updateCharacteristicDto)
        {
            try
            {
                var updateCharacteristic = await _productRepository.UpdateCharacteristic(charId, updateCharacteristicDto);
                if (updateCharacteristic == null)
                {
                    return NoContent();
                }
                var response = await _productRepository.GetCharacteristic(charId);
                return Ok(response.ConvertToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////// Images //////////////

        [HttpDelete]
        [Route("characteristics/deleteCharacteristics/{id:long}")]
        public async Task<ActionResult<Status>> DeleteCharacteristic(long id)
        {
            try
            {
                var characteristic = await _productRepository.DeleteCharacteristic(id);
                return Ok(characteristic);
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }

        [HttpGet]
        [Route("{prodId:long}/getImages")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesFromProduct(long prodId)
        {
            try
            {
                var images = await _productRepository.GetImagesFromProduct(prodId);
                var imagesDto = images.ConvertToDto();
                return Ok(imagesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("characteristics/addNewImage")]
        public async Task<IActionResult> AddNewImage([FromBody] ImageDto image)
        {
            try
            {
                var newImage = await _productRepository.AddNewImage(image);
                if (newImage == null)
                {
                    return NoContent();
                }
                return Ok(newImage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("images/updateImages/{charId:long}")]
        public async Task<IActionResult> UpdateImage(long imgId, UpdateImageDto updateImageDto)
        {
            try
            {
                var updateImage = await _productRepository.UpdateImage(imgId, updateImageDto);
                if (updateImage == null)
                {
                    return NoContent();
                }
                var response = await _productRepository.GetImage(imgId);
                return Ok(response.ConvertToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("images/deleteImage/{id:long}")]
        public async Task<ActionResult<Status>> DeleteImage(long id)
        {
            try
            {
                var image = await _productRepository.DeleteImage(id);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }
        //////////// Descriptions /////////////
        ///
        [HttpGet]
        [Route("{prodId:long}/getDescriptions")]
        public async Task<ActionResult<List<DescriptionDto>>> GetDescriptionsFromProduct(long prodId)
        {
            try
            {
                var descriptions = await _productRepository.GetDescriptionsFromProduct(prodId);
                var descriptionsDto = descriptions.ConvertToDto();
                return Ok(descriptionsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("descriptions/addNewDescription")]
        public async Task<IActionResult> AddNewDescription([FromBody] DescriptionDto descriptionDto)
        {
            try
            {
                var newDescription = await _productRepository.AddNewDescription(descriptionDto);
                if (newDescription == null)
                {
                    return NoContent();
                }
                return Ok(newDescription);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка создания");
            }
        }

        [HttpPut]
        [Route("descriptions/updateDescription/{charId:long}")]
        public async Task<IActionResult> UpdateDescription(long descId, UpdateDescriptionDto updateDescDto)
        {
            try
            {
                var updateDescription = await _productRepository.UpdateDescription(descId, updateDescDto);
                if (updateDescDto == null)
                {
                    return NoContent();
                }
                var response = await _productRepository.GetDescription(descId);
                return Ok(response.ConvertToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("description/deleteDescription/{id:long}")]
        public async Task<ActionResult<Status>> DeleteDescription(long id)
        {
            try
            {
                var description = await _productRepository.DeleteDescription(id);
                return Ok(description);
            }
            catch (Exception ex)
            {
                return new Status { Message = ex.Message, StatusCode = 500 };
            }
        }
    }
}
