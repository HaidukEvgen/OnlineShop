using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.AuthAPI.AppData;
using OnlineShop.Services.AuthAPI.MiddlewareHandlers;
using OnlineShop.Services.AuthAPI.Models.Data;
using OnlineShop.Services.AuthAPI.Services.Implementations;
using OnlineShop.Services.AuthAPI.Services.Interfaces;

namespace OnlineShop.Services.AuthAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMsSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("ConnectionString");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtOptions>(config.GetSection("ApiSettings:JwtOptions"));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }

        public static void ApplyMigrations(this IApplicationBuilder app, IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }
        }
    }
}
