using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetBootcamp.API.ExceptionHandlers;

namespace bootcamp.Service.ExceptionHandlers
{
    public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                logger.LogInformation($"Hata mesajı gönderildi(sms): {exception.Message}");
            }


            return ValueTask.FromResult(false);
        }
    }
}