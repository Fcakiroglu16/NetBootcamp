namespace LoggingAndMiddleware.API.Models
{
    public class ExceptionSaveToDatabase : Exception
    {
        public ExceptionSaveToDatabase(string message) : base(message)
        {
        }
    }
}