using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Services.AuthAPI.Exceptions;
using OnlineShop.Services.AuthAPI.Models.Data;
using OnlineShop.Services.AuthAPI.Models.Dto;
using OnlineShop.Services.AuthAPI.Repositories.Interfaces;
using OnlineShop.Services.AuthAPI.Services.Interfaces;

namespace OnlineShop.Services.AuthAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registrationRequestDto);

            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = await _userRepository.GetByEmailAsync(registrationRequestDto.Name);

                var userDto = _mapper.Map<UserDto>(userToReturn);

                return new ResponseDto { Message = "User registered successfuly", Result = userDto };
            }
            else
            {
                throw new RegisterException(result.Errors.FirstOrDefault().Description);
            }
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequestDto.UserName);

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                throw new LoginException("Username or password is incorrect");
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var userDto = _mapper.Map<UserDto>(user);

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return new ResponseDto { Message = "User logged in successfuly", Result = loginResponseDto };
        }

        public async Task<ResponseDto> AssignRoleAsync(string email, string roleName)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return new ResponseDto { Message = "Role assigned successfuly" };
            }
            else
            {
                throw new AssignRoleException("Error while assigning a role");
            }
        }
    }
}
