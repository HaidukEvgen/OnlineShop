using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Auth.Api.MiddlewareHandlers;
using OnlineShop.Services.Auth.BusinessLayer.Mapper;
using OnlineShop.Services.Auth.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Auth.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Auth.DataLayer.AppData;
using OnlineShop.Services.Auth.DataLayer.Models.Data;
using OnlineShop.Services.Auth.DataLayer.Repositories.Implementations;
using OnlineShop.Services.Auth.DataLayer.Repositories.Interfaces;

namespace OnlineShop.Services.Auth.Api.Extensions
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
