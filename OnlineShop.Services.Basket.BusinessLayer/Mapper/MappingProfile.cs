using AutoMapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.DataLayer.Models.Data;

namespace OnlineShop.Services.Basket.BusinessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart, BasketDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<ShoppingCartItem, BasketItemDto>();

            CreateMap<UpdateBasketDto, ShoppingCart>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<BasketItemDto, ShoppingCartItem>();

            CreateMap<ShoppingCart, OrderCreateDto>()
               .ForMember(dest => dest.UserId, opt => opt.Ignore())
               .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.Items.Select(item => item.ProductId)))
               .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<OrderDetailsDto, OrderCreateDto>();
        }
    }
}
