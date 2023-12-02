using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Services.Auth.DataLayer.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
