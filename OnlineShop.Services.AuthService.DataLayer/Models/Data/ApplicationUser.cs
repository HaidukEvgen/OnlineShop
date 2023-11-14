using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Services.AuthService.DataLayer.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
