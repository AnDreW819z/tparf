using tparf.api.Entities;
using tparf.dto.Auth;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Images;
using tparf.dto.Product;
using tparf.dto.Product.Descriptions;

namespace tparf.api.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        public Task<Product> GetProductByName(string name);
        Task<Product> GetProduct(long id);
        public Task<Product> AddNewProduct(CreateProductDto productDto);
        public Task<Product> UpdateProduct(long id, UpdateProductDto productDto);
        public Task<Status> DeleteProduct(long id);
        public Task<Status> DeleteAllProducts();

        Task<Characteristic> GetCharacteristic(long id);
        public Task<List<Characteristic>> GetCharacteristicsFromProduct(long productId);
        public Task<Characteristic> AddNewCharacteristic(CharacteristicDto characteristic);
        public Task<Characteristic> UpdateCharacteristic(long charId, UpdateCharacteristicDto updateCharacteristic);
        public Task<Status> DeleteCharacteristic(long charId);

        public Task<ProductImage> GetImage(long id);
        public Task<List<ProductImage>> GetImagesFromProduct(long productId);
        public Task<ProductImage> AddNewImage(ImageDto imageDto);
        public Task<ProductImage> UpdateImage(long imgId, UpdateImageDto updateImage);
        public Task<Status> DeleteImage(long charId);

        public Task<ProductDescription> GetDescription(long id);
        public Task<List<ProductDescription>> GetDescriptionsFromProduct(long productId);
        public Task<ProductDescription> AddNewDescription(DescriptionDto descDto);
        public Task<ProductDescription> UpdateDescription(long descId, UpdateDescriptionDto updateImage);
        public Task<Status> DeleteDescription(long descId);
    }
}
