using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.Extensions.Configuration;
using OnlineShop.Services.Order.Api.Extensions;
using OnlineShop.Services.Order.BusinessLayer.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureBusinessServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.Configure<EmailSetupOptions>(builder.Configuration.GetSection("EmailSetup"));

var app = builder.Build();

app.AppendGlobalErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AppendHangfireDashboard(builder.Configuration);

app.ApplyMigrations(app.Services);

app.Run();
