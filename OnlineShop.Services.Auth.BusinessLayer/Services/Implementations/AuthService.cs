using AutoMapper;
using OnlineShop.Services.Auth.BusinessLayer.Exceptions;
using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Auth.DataLayer.Models.Data;
using OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Auth.BusinessLayer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registrationRequestDto);

            var result = await _userRepository.RegisterAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = await _userRepository.GetByNameAsync(registrationRequestDto.Name);

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
            var user = await _userRepository.GetByNameAsync(loginRequestDto.UserName);

            var isValid = await _userRepository.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                throw new LoginException("Username or password is incorrect");
            }

            var roles = await _userRepository.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var userDto = _mapper.Map<UserDto>(user);

            var loginResponseDto = new LoginResponseDto
            {
                User = userDto,
                Token = token
            };

            return new ResponseDto { Message = "User logged in successfuly", Result = loginResponseDto };
        }

        public async Task<ResponseDto> AssignRoleAsync(string name, string roleName)
        {
            var user = await _userRepository.GetByNameAsync(name);
            if (user != null)
            {
                var isRoleExist = await _userRepository.RoleExistsAsync(roleName);
                if (!isRoleExist)
                {
                    await _userRepository.CreateRoleAsync(roleName);
                }

                await _userRepository.AddToRoleAsync(user, roleName);

                return new ResponseDto { Message = "Role assigned successfuly" };
            }
            else
            {
                throw new AssignRoleException("Error while assigning a role");
            }
        }
    }
}
