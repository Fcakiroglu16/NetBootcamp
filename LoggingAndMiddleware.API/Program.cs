using System.Net;
using LoggingAndMiddleware.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(x =>
{
    x.Run(context =>
    {
        var logger = x.ApplicationServices.GetRequiredService<ILogger<Program>>();
        var loggerFactory = x.ApplicationServices.GetRequiredService<ILoggerFactory>();
        var customLogger = loggerFactory.CreateLogger("CustomLogger");


        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerPathFeature!.Error;

        if (exception is ExceptionSaveToDatabase)
        {
            using (var scope = x.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var error = new Error
                {
                    Message = exception.Message,

                    Created = DateTime.Now
                };

                dbContext.Errors.Add(error);
                dbContext.SaveChanges();
            }
        }


        logger.LogError(exception, exception.Message);


        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsJsonAsync(new
        {
            Data = string.Empty, Errors = new List<string>()
            {
                exception.Message
            }
        });
    });
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();