namespace OnlineShop.Services.Auth.BusinessLayer.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
