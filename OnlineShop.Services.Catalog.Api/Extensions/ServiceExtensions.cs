﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OnlineShop.Services.Catalog.Api.MiddlewareHandlers;
using OnlineShop.Services.Catalog.Application.Mapper;
using OnlineShop.Services.Catalog.Application.Services.Implementations;
using OnlineShop.Services.Catalog.Application.Services.Interfaces;
using OnlineShop.Services.Catalog.Application.Validators;
using OnlineShop.Services.Catalog.Domain.Repositories.Interfaces;
using OnlineShop.Services.Catalog.Infrastructure.Data;
using OnlineShop.Services.Catalog.Infrastructure.Data.Implementations;
using OnlineShop.Services.Catalog.Infrastructure.Data.Interfaces;
using OnlineShop.Services.Catalog.Infrastructure.Repositories.Implementations;

namespace OnlineShop.Services.Catalog.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CatalogDbOptions>(config.GetSection("ApiSettings:CatalogDatabaseOptions"));
        }

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

        public static void AppendGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalErrorHandler>();
        }

        public static void AddKestrelConfiguration(this IWebHostBuilder builder, IConfiguration config)
        {
            var password = config["Kestrel:Certificates:Development:Password"];

            builder.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(443, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    listenOptions.UseHttps("/root/.aspnet/https/OnlineShop.Services.Catalog.Api.pfx", password);
                });
                options.ListenAnyIP(8001, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                    listenOptions.UseHttps("/root/.aspnet/https/OnlineShop.Services.Catalog.Api.pfx", password);
                });
                options.ListenAnyIP(80, o => o.Protocols = HttpProtocols.Http1AndHttp2);
            });
        }
    }
}
