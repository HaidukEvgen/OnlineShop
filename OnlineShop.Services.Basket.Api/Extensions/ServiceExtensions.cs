﻿using FluentValidation.AspNetCore;
using FluentValidation;
using OnlineShop.Services.Basket.Api.MiddlewareHandlers;
using OnlineShop.Services.Basket.BusinessLayer.Mapper;
using OnlineShop.Services.Basket.BusinessLayer.Services.Implementations;
using OnlineShop.Services.Basket.BusinessLayer.Services.Interfaces;
using OnlineShop.Services.Basket.DataLayer.Repositories.Implementations;
using OnlineShop.Services.Basket.DataLayer.Repositories.Interfaces;
using OnlineShop.Services.Basket.BusinessLayer.Validators;

namespace OnlineShop.Services.Basket.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureReddis(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(options =>
             {
                 options.Configuration = config.GetConnectionString("RedisUrl");
             });
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(UpdateBasketDtoValidator).Assembly);
        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }
    }
}
