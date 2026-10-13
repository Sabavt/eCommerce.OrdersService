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

public class RabbitMQProductUpdationConsumer : RabbitMQConsumerBase, IRabbitMQProductUpdationConsumer 
{ 
    private readonly ILogger<RabbitMQProductUpdationConsumer> _logger; 

    public RabbitMQProductUpdationConsumer(ILogger<RabbitMQProductUpdationConsumer> logger, IDistributedCache distributedCache, IConfiguration configuration) : base(distributedCache, configuration)
    { 
        _logger = logger; 
    } 

    public async Task Consume()
    {
        string routingKey = "products.update.ProductName";
        string queueName = "orders.product.update.ProductName.queue";

        await _channel.ExchangeDeclareAsync(_configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, ExchangeType.Direct, true);

        await _channel.QueueDeclareAsync(queueName, true);
        await _channel.QueueBindAsync(queueName, _configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            var updated_product = JsonSerializer.Deserialize<ProductsMicroserviceProducerMessage>(message);

            _logger.LogInformation($"product name updated - {updated_product?.ProductName}");
            await _cache.SetStringAsync($"product:{updated_product?.ProductName}", updated_product!.ProductName);
        };

        await _channel.BasicConsumeAsync(queueName, true, consumer);
    } 
}