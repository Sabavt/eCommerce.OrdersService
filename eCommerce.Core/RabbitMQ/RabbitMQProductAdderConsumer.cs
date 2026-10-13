using eCommerce.Core.RabbitMQ.Contracts;
using eCommerce.Core.Redis; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQProductAdderConsumer(ILogger<RabbitMQProductAdderConsumer> logger, RedisCacheService distributedCache, IConfiguration configuration) : RabbitMQConsumerBase(configuration, distributedCache), IRabbitMQAdderConsumer
{
    private readonly ILogger<RabbitMQProductAdderConsumer> _logger = logger;

    public Task Consume()
    {
        throw new NotImplementedException();
    }
}