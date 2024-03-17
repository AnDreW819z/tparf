using Microsoft.EntityFrameworkCore;
using tparf.api.Data;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Images;
using tparf.dto.Product;
using tparf.dto.Product.Descriptions;
using System;

namespace tparf.api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TparfDbContext _tparfDbContext;

        public ProductRepository(TparfDbContext tparfDbContext)
        {
            _tparfDbContext = tparfDbContext;
        }

        private async Task<bool> ProductExist(long productId)
        {
            return await _tparfDbContext.Products.AnyAsync(c => c.Id == productId);
        }

        private async Task<bool> CharacteristicExist(long charId)
        {
            return await _tparfDbContext.Characteristics.AnyAsync(c => c.Id == charId);
        }

        private async Task<bool> ImageExist(long imgId)
        {
            return await _tparfDbContext.ProductImages.AnyAsync(c => c.Id == imgId);
        }

        private async Task<bool> DescriptionExist(long desId)
        {
            return await _tparfDbContext.Descriptions.AnyAsync(c => c.Id == desId);
        }

        public async Task<Product> AddNewProduct(CreateProductDto productDto)
        {
            if (await ProductExist(productDto.Id) == false)
            {
                Product product = new Product
                {
                    Name = productDto.Name,
                    Article = productDto.Article,
                    ImageUrl = productDto.ImageUrl,
                    Price = productDto.Price,
                    Discount = productDto.Discount,
                    ManufacturerId= productDto.ManufacturerId,
                    SubcategoryId = productDto.SubcategoryId
                };
                if (product != null)
                {
                    await _tparfDbContext.Products.AddAsync(product);
                    await _tparfDbContext.SaveChangesAsync();
                    return product;
                }
            }
            return null;
        }

        public async Task<Status> DeleteProduct(long id)
        {
            Product product = await _tparfDbContext.Products.FindAsync(id);
            if (product != null)
            {
                _tparfDbContext.Products.Remove(product);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Товар успешно удален", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }


        public async Task<Product> GetProduct(long id)
        {

            if (await ProductExist(id))
            {
                var product = await _tparfDbContext.Products.SingleOrDefaultAsync(c => c.Id == id);
                product.Manufacturer = await _tparfDbContext.Manufacturers.FindAsync(product.ManufacturerId);
                product.Subcategory = await _tparfDbContext.Subcategories.FindAsync(product.SubcategoryId);
                if(product.Images == null || product.Characteristics== null || product.Descriptions == null)
                {
                    product.Characteristics = await GetCharacteristicsFromProduct(id);
                    product.Images= await GetImagesFromProduct(id);
                    product.Descriptions= await GetDescriptionsFromProduct(id);
                }
                return product;
            }
            return default;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _tparfDbContext.Products.Include(p => p.Subcategory).Include(p=> p.Manufacturer).ToListAsync();
            return products;
        }

        public async Task<Product> UpdateProduct(long id, UpdateProductDto productDto)
        {
            var product = await _tparfDbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.Name = productDto.Name;
                product.Article = productDto.Article;
                product.ImageUrl = productDto.ImageUrl;
                product.Price = productDto.Price;
                product.Discount = productDto.Discount;
                product.SubcategoryId = productDto.SubcategoryId;
                await _tparfDbContext.SaveChangesAsync();
                return product;
            }
            return default;
        }

        public async Task<List<Characteristic>> GetCharacteristicsFromProduct(long productId)
        {
            var characteristics = await _tparfDbContext.Characteristics.Include(c => c.Product).Where(c => c.ProductId == productId).ToListAsync();
            return characteristics;
        }

        public async Task<Characteristic> AddNewCharacteristic(CharacteristicDto characteristicDto)
        {
            if (await CharacteristicExist(characteristicDto.Id) == false)
            {
                Characteristic characteristic = new Characteristic
                {
                    Name = characteristicDto.Name,
                    Value = characteristicDto.Value,
                    ProductId = characteristicDto.ProductId,
                };
                if (characteristic != null)
                {
                    await _tparfDbContext.Characteristics.AddAsync(characteristic);
                    await _tparfDbContext.SaveChangesAsync();
                    return characteristic;
                }
            }
            return default;
        }

        public async Task<Characteristic> UpdateCharacteristic(long charId, UpdateCharacteristicDto updateCharacteristic)
        {
            Characteristic characteristic = await _tparfDbContext.Characteristics.FindAsync(charId);
            if (characteristic != null)
            {
                characteristic.Name = updateCharacteristic.Name;
                characteristic.Value = updateCharacteristic.Value;
                await _tparfDbContext.SaveChangesAsync();
                return characteristic;
            }
            return default;
        }

        public async Task<Status> DeleteCharacteristic(long charId)
        {
            Characteristic characteristic = await _tparfDbContext.Characteristics.FindAsync(charId);
            if (characteristic != null)
            {
                _tparfDbContext.Characteristics.Remove(characteristic);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Характеристика успешно удалена", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<Characteristic> GetCharacteristic(long id)
        {
            if (await CharacteristicExist(id))
            {
                var characteristic = await _tparfDbContext.Characteristics.SingleOrDefaultAsync(c => c.Id == id);
                characteristic.ProductId = GetProduct(characteristic.ProductId).Result.Id;
                return characteristic;
            }
            return default;
        }

        public async Task<ProductImage> AddNewImage(ImageDto imageDto)
        {
            if (await ImageExist(imageDto.Id) == false)
            {
                ProductImage image = new ProductImage
                {
                    Value = imageDto.Value,
                    ProductId = imageDto.ProductId,
                };
                if (image != null)
                {
                    await _tparfDbContext.ProductImages.AddAsync(image);
                    await _tparfDbContext.SaveChangesAsync();
                    return image;
                }
            }
            return default;
        }

        public async Task<Status> DeleteImage(long imgId)
        {
            ProductImage image = await _tparfDbContext.ProductImages.FindAsync(imgId);
            if (image != null)
            {
                _tparfDbContext.ProductImages.Remove(image);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Картинка успешно удалена", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }

        public async Task<ProductImage> GetImage(long id)
        {
            if (await ImageExist(id))
            {
                var image = await _tparfDbContext.ProductImages.SingleOrDefaultAsync(c => c.Id == id);
                image.ProductId = GetProduct(image.ProductId).Result.Id;
                return image;
            }
            return default;
        }

        public async Task<List<ProductImage>> GetImagesFromProduct(long productId)
        {
            var images = await _tparfDbContext.ProductImages.Include(c => c.Product).Where(c => c.ProductId == productId).ToListAsync();
            return images;
        }

        public async Task<ProductImage> UpdateImage(long imgId, UpdateImageDto updateImage)
        {
            ProductImage image = await _tparfDbContext.ProductImages.FindAsync(imgId);
            if (image != null)
            {
                image.Value = updateImage.Value;
                await _tparfDbContext.SaveChangesAsync();
                return image;
            }
            return default;
        }

        public async Task<ProductDescription> GetDescription(long id)
        {
            if (await DescriptionExist(id))
            {
                var description = await _tparfDbContext.Descriptions.SingleOrDefaultAsync(c => c.Id == id);
                description.ProductId = GetProduct(description.ProductId).Result.Id;
                return description;
            }
            return default;
        }

        public async Task<List<ProductDescription>> GetDescriptionsFromProduct(long productId)
        {
            var descriptions = await _tparfDbContext.Descriptions.Include(c => c.Product).Where(c => c.ProductId == productId).ToListAsync();
            return descriptions;
        }

        public async Task<ProductDescription> AddNewDescription(DescriptionDto descDto)
        {
            if (await DescriptionExist(descDto.Id) == false)
            {
                ProductDescription description = new ProductDescription
                {
                    Title = descDto.Title,
                    Text= descDto.Text,
                    ProductId = descDto.ProductId,
                };
                if (description != null)
                {
                    await _tparfDbContext.Descriptions.AddAsync(description);
                    await _tparfDbContext.SaveChangesAsync();
                    return description;
                }
            }
            return default;
        }

        public async Task<ProductDescription> UpdateDescription(long descId, UpdateDescriptionDto updateDescription)
        {
            ProductDescription description = await _tparfDbContext.Descriptions.FindAsync(descId);
            if (description != null)
            {
                description.Title = updateDescription.Title;
                description.Text = updateDescription.Text;
                await _tparfDbContext.SaveChangesAsync();
                return description;
            }
            return default;
        }

        public async Task<Status> DeleteDescription(long descId)
        {
            ProductDescription description = await _tparfDbContext.Descriptions.FindAsync(descId);
            if (description != null)
            {
                _tparfDbContext.Descriptions.Remove(description);
                await _tparfDbContext.SaveChangesAsync();
                return new Status { Message = "Описание успешно удалено", StatusCode = 200 };
            }
            return new Status { Message = "Ошибка удаления", StatusCode = 500 };
        }
    }

}
