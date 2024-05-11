using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using NetBootcamp.API.Products;
using NetBootcamp.API.Products.ProductCreateUseCase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateRequestValidator>();


// Add services to the container.


// DI(Dependency Injection) Container framework
// IoC ( Inversion Of Container)  framework
//  - Dependency Inversion / Inversion Of Control Principles
//  - Dependency Injection Design Pattern


// 1. AddSingleton
// 2. AddScoped (*)
// 3. AddTransient

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<PriceCalculator>();


var app = builder.Build();

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