using AutoMapper;
using tparf.Dto;
using tparf.Models;

namespace tparf.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ManufacturerDto, Manufacturer>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductProperty, ProductPropertyDto>();
            CreateMap<ProductPropertyDto, ProductProperty>();
            //CreateMap<ApplicationUser, UserDto>();
            //CreateMap<UserDto, ApplicationUser>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Subcategory, SubcategoryDto>();
            CreateMap<SubcategoryDto, Subcategory>();
        }
        
    }
}
