using AuthService.API;
using AuthService.API.ServiceExtension;
using AuthService.Core.Interfaces;
using AuthService.Core.Services;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var connectionString = config["ConnectionStrings:DefaultConnection"];

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
    .Build();

// Configure JWT authentication
builder.Services.ConfigureJwtAuth(config);

// Add services to the container.
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureBadRequest();
builder.Services.ConfigureSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

var app = builder.Build();

//For test only
AppDbInitializer.Seed(app);

//For test only
AppDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
