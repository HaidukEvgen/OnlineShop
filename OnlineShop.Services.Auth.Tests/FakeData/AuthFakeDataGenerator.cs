using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;

namespace OnlineShop.Services.Auth.Tests.FakeData
{
    public class AuthFakeDataGenerator
    {
        private readonly string[] _roles = ["user", "admin"];

        public LoginRequestDto GenerateFakeLoginRequestDto()
        {
            return new Faker<LoginRequestDto>()
                .RuleFor(loginDto => loginDto.UserName, faker => faker.Person.Email)
                .RuleFor(loginDto => loginDto.Password, faker => faker.Internet.GenerateCustomPassword())
                .Generate();
        }

        public RegistrationRequestDto GenerateFakeRegistrationRequestDto()
        {
            return new Faker<RegistrationRequestDto>()
                .RuleFor(registerDto => registerDto.Name, faker => faker.Person.FullName)
                .RuleFor(registerDto => registerDto.Email, faker => faker.Person.Email)
                .RuleFor(registerDto => registerDto.PhoneNumber, faker => faker.Person.Phone)
                .RuleFor(registerDto => registerDto.Password, faker => faker.Internet.GenerateCustomPassword())
                .Generate();
        }

        public AssignRoleRequestDto GenerateFakeAssignRoleRequestDto()
        {
            return new Faker<AssignRoleRequestDto>()
                .RuleFor(assignRoleDto => assignRoleDto.Name, faker => faker.Person.UserName)
                .RuleFor(assignRoleDto => assignRoleDto.Role, faker => faker.Random.ArrayElement(_roles))
                .Generate();
        }

        public UserDto GenerateFakeUserDto()
        {
            return new Faker<UserDto>()
                .RuleFor(userDto => userDto.Id, faker => faker.Random.Guid().ToString())
                .RuleFor(userDto => userDto.Email, faker => faker.Person.Email)
                .RuleFor(userDto => userDto.Name, faker => faker.Person.FullName)
                .RuleFor(userDto => userDto.PhoneNumber, faker => faker.Person.Phone)
                .Generate();
        }

        public IList<string> GenerateFakeRoles()
        {
            return new Faker().Random.ArrayElements(_roles);
        }
    }
}
