using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Services.AuthAPI.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
