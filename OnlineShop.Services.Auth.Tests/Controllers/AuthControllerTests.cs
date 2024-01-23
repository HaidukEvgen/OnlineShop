using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Auth.Api.Controllers;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Auth.Tests.FakeData;

namespace OnlineShop.Services.Auth.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly IAuthService _mockAuthService;
        private readonly AuthFakeDataGenerator _fakeData;
        public AuthControllerTests()
        {
            _mockAuthService = Substitute.For<IAuthService>();
            _controller = new AuthController(_mockAuthService);
            _fakeData = new AuthFakeDataGenerator();
        }

        [Fact]
        public async Task Register_WhenModelIsValid_ShouldReturnOk()
        {
            // Arrange
            var registerDto = _fakeData.GenerateFakeRegistrationRequestDto();
            var cancellationToken = CancellationToken.None;

            await _mockAuthService.RegisterAsync(registerDto, cancellationToken);

            // Act
            var result = await _controller.RegisterAsync(registerDto, cancellationToken);

            // Assert
            result
                .Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Login_WhenModelIsValid_ShouldReturnNoContent()
        {
            // Arrange
            var loginDto = _fakeData.GenerateFakeLoginRequestDto();
            var cancellationToken = CancellationToken.None;

            await _mockAuthService.LoginAsync(loginDto, cancellationToken);

            // Act
            var result = await _controller.LoginAsync(loginDto, cancellationToken);

            // Assert
            result
                .Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task AssignRole_WhenModelIsValid_ShouldReturnNoContent()
        {
            // Arrange
            var assignRoleDto = _fakeData.GenerateFakeAssignRoleRequestDto();
            var cancellationToken = CancellationToken.None;

            await _mockAuthService.AssignRoleAsync(assignRoleDto.Name, assignRoleDto.Role, cancellationToken);

            // Act
            var result = await _controller.AssignRoleAsync(assignRoleDto, cancellationToken);

            // Assert
            result
                .Should().BeOfType<OkObjectResult>();
        }
    }
}
