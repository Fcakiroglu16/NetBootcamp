using System.Net;
using bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace NetBootcamp.API.Extensions
{
    public static class MiddlewareExt
    {
        public static void AddMiddlewares(this WebApplication app)
        {
            app.UseExceptionHandler();
            //app.UseMiddleware<IpWhiteListMiddleware>();
            //app.UseExceptionHandler(appBuilder =>
            //{
            //    appBuilder.Run(async context =>
            //    {
            //        //var loggerFactory= context.RequestServices.GetService<ILoggerFactory>();

            //        // var logger= loggerFactory!.CreateLogger("GlobalExceptionLogger");

            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "application/json";
            //        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();


            //        if (contextFeature != null)
            //        {
            //            var exception = contextFeature.Error;


            //            var responseModel =
            //                ResponseModelDto<NoContent>.Fail(exception.Message, HttpStatusCode.InternalServerError);


            //            await context.Response.WriteAsJsonAsync(responseModel);
            //        }
            //    });
            //});


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("1. middleware request\n");
            //    //request
            //    await next();
            //    await context.Response.WriteAsync("1. middleware response\n");
            //    //response
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("2. middleware request\n");
            //    //request
            //    await next();
            //    await context.Response.WriteAsync("2. middleware response\n");
            //    //response
            //});


            //app.Run(async context => { await context.Response.WriteAsync("terminal middleware\n"); });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}