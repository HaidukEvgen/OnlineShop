using AutoMapper;
using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;
using OnlineShop.Services.Auth.DataLayer.Models.Data;

namespace OnlineShop.Services.Auth.BusinessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationRequestDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedEmail, act => act.MapFrom(src => src.Email.ToUpper()));
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
