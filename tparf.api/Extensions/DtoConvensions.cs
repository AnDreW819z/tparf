using tparf.api.Entities;
using tparf.dto.CartItems;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;
using tparf.dto.Orders;
using tparf.dto.Product;
using tparf.dto.Product.Characteristics;
using tparf.dto.Product.Descriptions;
using tparf.dto.Product.Images;
using tparf.dto.Subcategories;

namespace tparf.api.Extensions
{
    public static class DtoConvensions
    {
        ///////// Category /////////
        public static List<CategoryDto> ConvertToDto(this List<Category> categories)
        {
            if (categories != null)
            {
                return (from category in categories
                        select new CategoryDto
                        {
                            Id = category.Id,
                            Name = category.Name,
                            IconCss = category.IconCss,
                            ImageUrl = category.ImageUrl,
                            ParentId = category.ParentId,
                            Children = ConvertToDto(category.Children)
                        }).ToList();
            }
            return null;
        }

        public static CategoryDto ConverToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                IconCss = category.IconCss,
                ImageUrl = category.ImageUrl,
                ParentId = category.ParentId,
                Children = ConvertToDto(category.Children)
            };
        }

        ///////////// Manufacturer ///////////////
        public static List<ManufacturerDto> ConvertToDto(this List<Manufacturer> manufacturers)
        {
            return (from manufacturer in manufacturers
                    select new ManufacturerDto
                    {
                        Id = manufacturer.Id,
                        Name = manufacturer.Name,
                        IconCss = manufacturer.IconCss,
                        ImageUrl = manufacturer.ImageUrl,
                    }).ToList();
        }

        public static ManufacturerDto ConverToDto(this Manufacturer manufacturer)
        {
            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                IconCss = manufacturer.IconCss,
                ImageUrl = manufacturer.ImageUrl,
            };
        }

        /////////// Products //////////////

        public static List<ProductDto> ConvertToDto(this List<Product> products)
        {
            return (from product in products
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Article = product.Article,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Discount = product.Discount,
                        ManufacturerId= product.Manufacturer.Id,
                        ManufacturerName = product.Manufacturer.Name,
                        CategoryId = product.Category.Id,
                        CategoryName = product.Category.Name
                    }).ToList();
        }

        public static ProductDtos ConvertToDto(this Product product)
        {
            return new ProductDtos
            {
                Id = product.Id,
                Name = product.Name,
                Article = product.Article,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Discount = product.Discount,
                ManufacturerId = product.Manufacturer.Id,
                ManufacturerName = product.Manufacturer.Name,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name,
                Characteristics = product.Characteristics.ConvertToDto(),
                Images= product.Images.ConvertToDto(),
                Descriptions= product.Descriptions.ConvertToDto(),

            };
        }

        /////////// Characteristict /////////////
        ///
        public static List<CharacteristicDto> ConvertToDto(this List<Characteristic> characteristics)
        {
            return (from characteristic in characteristics
                    select new CharacteristicDto
                    {
                        Id = characteristic.Id,
                        Name = characteristic.Name,
                        Value = characteristic.Value,
                        ProductId = characteristic.ProductId
                    }).ToList();
        }

        public static CharacteristicDto ConvertToDto(this Characteristic characteristic)
        {
            return new CharacteristicDto
            {
                Id = characteristic.Id,
                Name = characteristic.Name,
                Value = characteristic.Value,
                ProductId = characteristic.ProductId,
            };
        }

        //////////// ProductImages ////////////
        public static List<ImageDto> ConvertToDto(this List<ProductImage> images)
        {
            return (from image in images
                    select new ImageDto
                    {
                        Id = image.Id,
                        Value = image.Value,
                        ProductId = image.ProductId
                    }).ToList();
        }

        public static ImageDto ConvertToDto(this ProductImage image)
        {
            return new ImageDto
            {
                Id = image.Id,
                Value = image.Value,
                ProductId = image.ProductId,
            };
        }

        //////////// ProductDescriptions ////////////
        public static List<DescriptionDto> ConvertToDto(this List<ProductDescription> descriptions)
        {
            return (from description in descriptions
                    select new DescriptionDto
                    {
                        Id = description.Id,
                        Title = description.Title,
                        Text= description.Text,
                        ProductId = description.ProductId
                    }).ToList();
        }

        public static DescriptionDto ConvertToDto(this ProductDescription description)
        {
            return new DescriptionDto
            {
                Id = description.Id,
                Title = description.Title,
                Text = description.Text,
                ProductId = description.ProductId,
            };
        }

        //////////// Shopping Carts /////////////
        ///
        public static List<CartItemDto> ConvertToDto(this List<CartItem> cartItems,
                                                           List<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductImageUrl = product.ImageUrl,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Qty = cartItem.Qty,
                        TotalPrice = product.Price * cartItem.Qty
                    }).ToList();
        }

        public static CartItemDto ConvertToDto(this CartItem cartItem,
                                                    Product product)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductImageUrl = product.ImageUrl,
                Price = product.Price,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price * cartItem.Qty
            };
        }

        ////////////// Orders ////////////////
        ///

        public static List<OrderDto> ConvertToDto(this List<Order> orders)
        {
            return (from order in orders
                    select new OrderDto
                    {
                        Id = order.Id,
                        CartId = order.CartId,
                        Email = order.Email,
                        PhoneNumber = order.PhoneNumber,
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        TotalPrice = order.TotalPrice,
                        Adress = order.Adress,
                        StatusId = order.StatusId,
                    }).ToList();

        }

        public static OrderDto ConvertToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CartId = order.CartId,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                FirstName = order.FirstName,
                LastName = order.LastName,
                TotalPrice = order.TotalPrice,
                Adress = order.Adress,
                StatusId = order.StatusId,
            };

        }

        public static List<OrderItemDto> ConvertToDto(this List<OrderItem> orderItems)
        {
            return (from orderItem in orderItems
                    select new OrderItemDto
                    {
                        Id = orderItem.Id,
                        OrderId = orderItem.OrderId,
                        ProductId = orderItem.ProductId,
                        ProductName = orderItem.ProductName,
                        Qty = orderItem.Qty,
                        Price = orderItem.Price,
                        TotalPriceByOrderItem = orderItem.TotalPriceByOrderItem,
                    }).ToList();
        }

    }
}
