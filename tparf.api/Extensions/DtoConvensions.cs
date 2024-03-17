﻿using tparf.api.Entities;
using tparf.dto.Categories;
using tparf.dto.Manufacturer;
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
                ImageUrl = category.ImageUrl
            };
        }
        ////////////////////    Subcategory   ///////////////////////////
        public static List<SubcategoryDto> ConvertToDto(this List<Subcategory> subcategories)
        {
            return (from subcategory in subcategories
                    select new SubcategoryDto
                    {
                        Id = subcategory.Id,
                        Name = subcategory.Name,
                        IconCss = subcategory.IconCss,
                        ImageUrl = subcategory.ImageUrl,
                        CategoryId = subcategory.Category.Id,
                        CategoryName = subcategory.Category.Name,
                    }).ToList();
        }

        public static SubcategoryDto ConverToDto(this Subcategory subcategory)
        {
            return new SubcategoryDto
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                IconCss = subcategory.IconCss,
                ImageUrl = subcategory.ImageUrl,
                CategoryId = subcategory.Category.Id,
                CategoryName = subcategory.Category.Name
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
                        SubcategoryId = product.Subcategory.Id,
                        SubcategoryName = product.Subcategory.Name
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
                SubcategoryId = product.Subcategory.Id,
                SubcategoryName = product.Subcategory.Name,
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
    }
}