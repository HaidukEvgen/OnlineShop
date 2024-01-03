using FluentValidation;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services.Order.Api.Middleware;
using OnlineShop.Services.Order.BusinessLayer.Consumers;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Mapper;
using OnlineShop.Services.Order.BusinessLayer.Infrastructure.Validators;
using OnlineShop.Services.Order.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Order.BusinessLayer.Services.Interfaces;
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

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHangfireService, HangfireService>();
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

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BasketCheckoutConsumer>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }

        public static void AppendHangfireDashboard(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = configuration.GetSection("HangfireOptions:User").Value,
                        Pass = configuration.GetSection("HangfireOptions:Password").Value
                    }
                }
            });
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
