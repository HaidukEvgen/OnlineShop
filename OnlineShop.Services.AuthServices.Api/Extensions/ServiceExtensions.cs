using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.AuthService.Api.MiddlewareHandlers;
using OnlineShop.Services.AuthService.BusinessLayer.Mapper;
using OnlineShop.Services.AuthService.BusinessLayer.Services.Implementations;
using OnlineShop.Services.AuthService.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.AuthService.DataLayer.AppData;
using OnlineShop.Services.AuthService.DataLayer.Models.Data;
using OnlineShop.Services.AuthService.DataLayer.Repositories.Implementations;
using OnlineShop.Services.AuthService.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.AuthService.Api.Extensions
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
            services.AddScoped<IAuthService, AuthenticationService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
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
