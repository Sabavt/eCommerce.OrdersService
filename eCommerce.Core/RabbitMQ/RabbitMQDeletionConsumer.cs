using eCommerce.Core.RabbitMQ.Contracts;
using eCommerce.Core.RabbitMQ.Messages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQDeletionConsumer : RabbitMQConsumerBase, IRabbitMQDeletionConsumer
{ 
    private readonly ILogger<RabbitMQDeletionConsumer> _logger; 

    public RabbitMQDeletionConsumer(ILogger<RabbitMQDeletionConsumer> logger, IDistributedCache distributedCache, IConfiguration configuration) : base(distributedCache, configuration)
    { 
        _logger = logger; 
    } 

    public async Task Consume()
    {
        string routingKey = "products.delete.productid";
        string queueName = "orders.product.delete.ProductName.queue";

        await _channel.ExchangeDeclareAsync(_configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, ExchangeType.Direct, true);

        await _channel.QueueDeclareAsync(queueName, true);
        await _channel.QueueBindAsync(queueName, _configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, args) =>
        { 
            string message = Encoding.UTF8.GetString(args.Body.Span);

            var deleted_product = JsonSerializer.Deserialize<ProductDeletedMessage>(message);

            _logger.LogInformation($"product deleted - {deleted_product?.ProductID}");
            await _cache.RemoveAsync($"product:{deleted_product?.ProductID}");
        };

        await _channel.BasicConsumeAsync(queueName, true, consumer);
    } 
}