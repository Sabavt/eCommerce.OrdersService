using eCommerce.Core.RabbitMQ.Contracts;
using Microsoft.Extensions.Hosting;

namespace eCommerce.Core.RabbitMQ.HostedServices;

public class RabbitMQAdderConsumerHost(IRabbitMQAdderConsumer rabbitMQAdderConsumer) : IHostedService
{
    private readonly IRabbitMQAdderConsumer _rabbitMQAdderConsumer = rabbitMQAdderConsumer;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _rabbitMQAdderConsumer.Consume();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    { 
        ((RabbitMQConsumerBase)_rabbitMQAdderConsumer).Dispose();

        return Task.CompletedTask;
    }
} 