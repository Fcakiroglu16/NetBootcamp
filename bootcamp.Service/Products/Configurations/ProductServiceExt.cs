using Bootcamp.Repository.Products;
using bootcamp.Service.Products.AsyncMethods;
using bootcamp.Service.Products.Helpers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Products;
using NetBootcamp.API.Products.ProductCreateUseCase;

namespace bootcamp.Service.Products.Configurations
{
    public static class ProductServiceExt
    {
        public static void AddProductService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService2, ProductService2>();
            services.AddScoped<IProductRepository2, ProductRepository2>();


            //builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<ProductCreateRequestValidator>();
            services.AddScoped<NotFoundFilter>();

            services.AddSingleton<PriceCalculator>();
        }
    }
}