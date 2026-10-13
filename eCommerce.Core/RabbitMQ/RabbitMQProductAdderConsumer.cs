using eCommerce.Core.DTO;
using eCommerce.Core.RabbitMQ.Contracts;
using eCommerce.Core.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQProductAdderConsumer(ILogger<RabbitMQProductAdderConsumer> logger, RedisCacheService distributedCache, IConfiguration configuration) : RabbitMQConsumerBase(configuration, distributedCache), IRabbitMQAdderConsumer
{
    private readonly ILogger<RabbitMQProductAdderConsumer> _logger = logger;

    public async Task Consume()
    {
        string exchangeName = _configuration["RabbitMQ_ProductsMicroservice_Exchange"]!;
        string routeKey = "product.add";
        string queue = "product.add.queue";

        await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct, true);

        await _channel.QueueDeclareAsync(queue, true, false, false);
        await _channel.QueueBindAsync(queue, exchangeName, routeKey);

        AsyncEventingBasicConsumer consumer = new(_channel);

        consumer.ReceivedAsync += async (sender, content) =>
        {
            string message = Encoding.UTF8.GetString(content.Body.Span);
            var product = JsonSerializer.Deserialize<ProductDTO>(message);

            if (product is not null)
            {
                _logger.LogInformation($"Product added - {product.ProductName}");
                await _cache.SetStringAsync($"product:{product.ProductID}", product); 
            }
        };

        await _channel.BasicConsumeAsync(queue, true, consumer);
    }
}