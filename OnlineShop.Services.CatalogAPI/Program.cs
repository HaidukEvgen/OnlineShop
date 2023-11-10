using OnlineShop.Services.CatalogAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureBusinessServices();
builder.Services.ConfigureDbOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();
//builder.AddAppAuthetication();

builder.Services.AddAuthorization();

var app = builder.Build();

app.AppendGlobalErrorHandler();

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