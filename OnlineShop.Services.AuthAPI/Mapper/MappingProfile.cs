using AutoMapper;
using OnlineShop.Services.AuthAPI.Models.Data;
using OnlineShop.Services.AuthAPI.Models.Dto;

namespace OnlineShop.Services.AuthAPI.Mapper
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
