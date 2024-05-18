namespace LoggingAndMiddleware.API.Models
{
    public class Error
    {
        public int Id { get; set; }
        public string Message { get; set; } = default!;
        public DateTime Created { get; set; }
    }
}