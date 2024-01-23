using AutoMapper;
using Moq;
using OnlineShop.Services.Auth.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Auth.DataLayer.Models.Data;
using OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces;
using OnlineShop.Services.Auth.Tests.FakeData;
using OnlineShop.Services.Auth.Tests.Helpers;

namespace OnlineShop.Services.Auth.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IAuthService _authService;

        private readonly AuthFakeDataGenerator _fakeData;

        private readonly AuthServiceTestsHelper _helper;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _mockMapper = new Mock<IMapper>();

            _fakeData = new AuthFakeDataGenerator();

            _authService = new AuthService(
                _mockUserRepository.Object,
                _mockJwtTokenGenerator.Object,
                _mockMapper.Object);

            _helper = new AuthServiceTestsHelper(
                _mockUserRepository,
                _mockJwtTokenGenerator,
                _mockMapper);
        }

        [Fact]
        public async Task RegisterAsync_WhenInputIsValid_ShouldReturnSuccess()
        {
            // Arrange
            var registerDto = _fakeData.GenerateFakeRegistrationRequestDto();
            var userDto = _fakeData.GenerateFakeUserDto();
            var cancellationToken = CancellationToken.None;

            var applicationUser = new ApplicationUser();

            _helper.SetUpValidUserForRegister(applicationUser, registerDto, userDto);

            // Act
            var result = await _authService.RegisterAsync(registerDto, cancellationToken);

            // Assert
            result
                .Should().NotBeNull();

            result
                .Result.Should().NotBeNull();

            result
                .IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task LoginAsync_WhenInputIsValid_ShouldReturnSuccess()
        {
            // Arrange
            var loginDto = _fakeData.GenerateFakeLoginRequestDto();
            var userDto = _fakeData.GenerateFakeUserDto();
            var cancellationToken = CancellationToken.None;
            var roles = _fakeData.GenerateFakeRoles();

            var applicationUser = new ApplicationUser();

            _helper.SetUpValidUserForLogin(applicationUser, loginDto, userDto, roles);

            // Act
            var result = await _authService.LoginAsync(loginDto, cancellationToken);

            // Assert
            result
                .Should().NotBeNull();

            result
                .Result.Should().NotBeNull();

            result
                .IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task AssignRoleAsync_WhenInputIsValid_ShouldReturnSuccess()
        {

            var cancellationToken = CancellationToken.None;
            var role = _fakeData.GenerateFakeRoles()[0];

            var applicationUser = new ApplicationUser();

            _helper.SetUpValidAssignRole(applicationUser);

            // Act
            var result = await _authService.AssignRoleAsync(applicationUser.Email, role);

            // Assert
            result
                .Should().NotBeNull();

            result
                .IsSuccess.Should().BeTrue();
        }
    }
}
