namespace Bootcamp.Web.Models
{
    public class ResponseModelDto<T>
    {
        public T? Data { get; init; }

        public List<string>? FailMessages { get; init; }
    }
}