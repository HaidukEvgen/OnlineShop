using AutoMapper;
using OnlineShop.Services.CatalogAPI.Models.Data;
using OnlineShop.Services.CatalogAPI.Models.Dto;

namespace OnlineShop.Services.CatalogAPI.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<AddProductDto, Product>();
        }
    }
}
