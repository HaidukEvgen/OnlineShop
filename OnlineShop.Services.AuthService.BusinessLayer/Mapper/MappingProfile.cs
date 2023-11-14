using AutoMapper;
using OnlineShop.Services.AuthService.BusinessLayer.Models.Dto;
using OnlineShop.Services.AuthService.DataLayer.Models.Data;

namespace OnlineShop.Services.AuthService.BusinessLayer.Mapper
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
