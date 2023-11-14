using OnlineShop.Services.AuthService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMsSqlServerContext(builder.Configuration);
builder.Services.ConfigureJwtOptions(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureBusinessServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureAutoMapper();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.ApplyMigrations(app.Services);

app.Run();