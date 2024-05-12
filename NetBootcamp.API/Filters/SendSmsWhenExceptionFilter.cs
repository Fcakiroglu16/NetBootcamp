using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetBootcamp.API.Filters
{
    public class SendSmsWhenExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = false;

            Console.WriteLine($"Hata var. Sms gönderildi : {context.Exception.Message}");
        }
    }
}