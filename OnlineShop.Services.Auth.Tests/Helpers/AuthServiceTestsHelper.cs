using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Auth.DataLayer.Models.Data;
using OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Auth.Tests.Helpers
{
    public class AuthServiceTestsHelper
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly Mock<IMapper> _mockMapper;

        public AuthServiceTestsHelper(
            Mock<IUserRepository> mockUserRepository,
            Mock<IJwtTokenGenerator> mockJwtTokenGenerator,
            Mock<IMapper> mockMapper)
        {
            _mockUserRepository = mockUserRepository;
            _mockJwtTokenGenerator = mockJwtTokenGenerator;
            _mockMapper = mockMapper;
        }

        public void SetUpValidUserForRegister(ApplicationUser applicationUser,
            RegistrationRequestDto registerDto, UserDto userDto)
        {
            _mockMapper
                .Setup(mapper => mapper.Map<ApplicationUser>(registerDto))
                .Returns(applicationUser);

            _mockUserRepository
               .Setup(repository => repository.RegisterAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);

            _mockUserRepository
               .Setup(repository => repository.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(applicationUser);

            _mockMapper
                .Setup(mapper => mapper.Map<UserDto>(applicationUser))
                .Returns(userDto);
        }

        public void SetUpValidUserForLogin(ApplicationUser applicationUser,
             LoginRequestDto loginDto, UserDto userDto, IList<string> roles)
        {
            _mockUserRepository
               .Setup(repository => repository.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(applicationUser);

            _mockUserRepository
               .Setup(repository => repository.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .ReturnsAsync(true);

            _mockUserRepository
               .Setup(repository => repository.GetRolesAsync(It.IsAny<ApplicationUser>()))
               .ReturnsAsync(roles);

            _mockJwtTokenGenerator
                .Setup(jwtTokenGenerator => jwtTokenGenerator.GenerateToken(It.IsAny<ApplicationUser>(), roles))
                .Returns(string.Empty);

            _mockMapper
                .Setup(mapper => mapper.Map<UserDto>(applicationUser))
                .Returns(userDto);
        }

        public void SetUpValidAssignRole(ApplicationUser applicationUser)
        {
            _mockUserRepository
               .Setup(repository => repository.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(applicationUser);

            _mockUserRepository
               .Setup(repository => repository.RoleExistsAsync(It.IsAny<string>()))
               .ReturnsAsync(true);

            _mockUserRepository
               .Setup(repository => repository.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
               .Returns(Task.CompletedTask);
        }
    }
}
