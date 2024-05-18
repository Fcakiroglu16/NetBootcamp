using System.Net;
using bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace bootcamp.Service.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            var responseModel =
                ResponseModelDto<NoContent>.Fail(exception.Message, HttpStatusCode.InternalServerError);

            await httpContext.Response.WriteAsJsonAsync(responseModel, cancellationToken: cancellationToken);


            return true;
        }
    }
}

// success => response model
// fail => response model
// exception => response model
// validatin => response model