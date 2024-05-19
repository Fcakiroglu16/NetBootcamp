using bootcamp.Service.ExceptionHandlers;
using bootcamp.Service.Products.Configurations;
using bootcamp.Service.Token;
using bootcamp.Service.Users;
using bootcamp.Service.Weather;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetBootcamp.API.ExceptionHandlers;


namespace bootcamp.Service
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(x => { x.SuppressModelStateInvalidFilter = true; });
            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddProductService();

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOptions"));
            services.Configure<Clients>(configuration.GetSection("Clients"));
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<UserService>();
        }
    }
}