using OnlineShop.Services.Catalog.Api.Extensions;
using OnlineShop.Services.Catalog.Api.MiddlewareHandlers;
using OnlineShop.Services.Catalog.Application.Services.gRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.WebHost.AddKestrelConfiguration(builder.Configuration);
builder.Services.AddGrpc(options =>
{
    {
        options.Interceptors.Add<GrpcErrorInterceptor>();
    }
});
builder.Services.ConfigureBusinessServices();
builder.Services.ConfigureDbOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();
builder.Services.AddAuthorization();
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.AppendGlobalErrorHandler();
app.MapGrpcService<CatalogGrpcService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();