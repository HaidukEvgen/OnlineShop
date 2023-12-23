using AutoMapper;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Application.Protos;
using OnlineShop.Services.Catalog.Domain.Models.Data;

namespace OnlineShop.Services.Catalog.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<NewProductDto, Product>();

            CreateMap<BasketItem, GrpcProductDto>()
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src.PriceInCents / 100))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
