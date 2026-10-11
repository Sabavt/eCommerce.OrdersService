using Microsoft.Extensions.Hosting;

namespace eCommerce.Core.RabbitMQ.HostedServices;

public class RabbitMQConumerHostedService(IRabbitMQConsumer rabbitMQConsumer) : IHostedService
{
    private readonly IRabbitMQConsumer _rabbitMQConsumer = rabbitMQConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    { 
       await _rabbitMQConsumer.Consume();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMQConsumer.Dispose(true);

        return Task.CompletedTask;
    }
}