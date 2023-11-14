using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Services.CatalogAPI.Data.Implementations;
using OnlineShop.Services.CatalogAPI.Data.Interfaces;
using OnlineShop.Services.CatalogAPI.Infrastructure.Mapper;
using OnlineShop.Services.CatalogAPI.Infrastructure.Validators;
using OnlineShop.Services.CatalogAPI.MiddlewareHandlers;
using OnlineShop.Services.CatalogAPI.Models.Data;
using OnlineShop.Services.CatalogAPI.Repositories.Implementations;
using OnlineShop.Services.CatalogAPI.Repositories.Interfaces;
using OnlineShop.Services.CatalogAPI.Services.Implementations;
using OnlineShop.Services.CatalogAPI.Services.Interfaces;
using System.Text;

namespace OnlineShop.Services.CatalogAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CatalogDbOptions>(config.GetSection("ApiSettings:CatalogDatabaseOptions"));
        }

        //public static void ConfigureIdentity(this IServiceCollection services)
        //{
        //    services.AddIdentity<ApplicationUser, IdentityRole>()
        //    .AddEntityFrameworkStores<AppDbContext>()
        //    .AddDefaultTokenProviders();
        //}

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(AddProductDtoValidator).Assembly);
        }

        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("ApiSettings");

            var secret = settingsSection.GetValue<string>("Secret");
            var issuer = settingsSection.GetValue<string>("Issuer");
            var audience = settingsSection.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }
    }
}
