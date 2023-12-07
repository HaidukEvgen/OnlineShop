using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.Api.Middleware;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Mapper;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators;
using OnlineShop.Services.Order.DataLayer.AppData;
using OnlineShop.Services.Order.DataLayer.Repositories.Implementations;
using OnlineShop.Services.Order.DataLayer.Repositories.Interfaces;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace OnlineShop.Services.Order.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(OrderCreateDtoValidator).Assembly);
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }

        public static void ApplyMigrations(this IApplicationBuilder app, IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<OrderContext>();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }
        }
    }
}
