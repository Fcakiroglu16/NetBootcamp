using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace bootcamp.Service.Users
{
    public class BackgroundServiceEmailSender(
        Channel<UserCreatedEvent> channel,
        ILogger<BackgroundServiceEmailSender> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await channel.Reader.WaitToReadAsync(stoppingToken))
            {
                try
                {
                    var userCreatedEvent = await channel.Reader.ReadAsync(stoppingToken);

                    throw new Exception("db hatası");

                    logger.LogInformation($"Email gönderildi: {userCreatedEvent.Email}");
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
        }
    }
}