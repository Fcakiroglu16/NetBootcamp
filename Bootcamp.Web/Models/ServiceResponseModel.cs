using System.Net;

namespace Bootcamp.Web.Models
{
    public class ServiceResponseModel<T>
    {
        public T? Data { get; set; }

        public bool IsSuccess { get; set; }

        public List<string>? Errors { get; set; }


        public static ServiceResponseModel<T> Success(T data)
        {
            return new ServiceResponseModel<T>
            {
                Data = data,
                IsSuccess = true,
            };
        }


        public static ServiceResponseModel<T> Fail(List<string>? messages)
        {
            return new ServiceResponseModel<T>
            {
                IsSuccess = false,
                Errors = messages,
            };
        }

        public static ServiceResponseModel<T> Fail(string message)
        {
            return new ServiceResponseModel<T>
            {
                IsSuccess = false,
                Errors = new List<string> { message },
            };
        }
    }
}