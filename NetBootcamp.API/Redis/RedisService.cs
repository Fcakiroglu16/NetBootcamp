using StackExchange.Redis;

namespace NetBootcamp.API.Redis
{
    public class RedisService
    {
        public IDatabase Database;

        public RedisService(string url)
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(url);


            connectionMultiplexer.ConnectionFailed += ConnectionMultiplexer_ConnectionFailed;


            Database = connectionMultiplexer.GetDatabase(1);
        }

        private void ConnectionMultiplexer_ConnectionFailed(object? sender, ConnectionFailedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}