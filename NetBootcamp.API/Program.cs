using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NetBootcamp.API.Products;
using NetBootcamp.API.Products.AsyncMethods;
using NetBootcamp.API.Products.ProductCreateUseCase;
using NetBootcamp.API.Repositories;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Products.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


builder.Services.Configure<ApiBehaviorOptions>(x => { x.SuppressModelStateInvalidFilter = true; });

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers(x => x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddProductService();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();


app.SeedDatabase();

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