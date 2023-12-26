using AutoMapper;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using OnlineShop.Services.Basket.BusinessLayer.Protos;
using OnlineShop.Services.Basket.DataLayer.Models.Data;
using OnlineShop.Shared.MassTransit.Messages;

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

            CreateMap<ShoppingCart, OrderCreatedEvent>()
               .ForMember(dest => dest.UserId, opt => opt.Ignore())
               .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.Items.Select(item => item.ProductId)))
               .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<OrderDetailsDto, OrderCreatedEvent>();

            CreateMap<ShoppingCartItem, GrpcProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price * 100));

            CreateMap<BasketItem, GrpcProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PriceInCents))
                .ReverseMap();

            CreateMap<IEnumerable<GrpcProductDto>, AreValidBasketItemsRequest>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

        }
    }
}
