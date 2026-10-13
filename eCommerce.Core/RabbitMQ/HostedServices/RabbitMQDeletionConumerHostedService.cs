using Microsoft.Extensions.Hosting;

namespace eCommerce.Core.RabbitMQ.HostedServices;

public class RabbitMQDeletionConumerHostedService(IRabbitMQDeletionConsumer rabbitMQConsumer) : IHostedService
{
    private readonly IRabbitMQDeletionConsumer _rabbitMQConsumer = rabbitMQConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    { 
       await _rabbitMQConsumer.Consume();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        ((RabbitMQConsumerBase)_rabbitMQConsumer).Dispose(); 

        return Task.CompletedTask;
    }
}