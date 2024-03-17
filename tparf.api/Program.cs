using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using tparf.api.Data;
using tparf.api.Interfaces;
using tparf.api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Db
builder.Services.AddDbContextPool<TparfDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("tparfConnection")));

// Inject app Dependencies (Dependency Injection)
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
//builder.Services.AddTransient<ITokenService, TokenService>();
//builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
