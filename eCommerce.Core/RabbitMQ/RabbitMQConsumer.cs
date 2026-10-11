using eCommerce.Core.RabbitMQ.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQConsumer : IRabbitMQConsumer, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly ILogger<RabbitMQConsumer> _logger;

    public RabbitMQConsumer(IConfiguration configuration, IConnection connection, IChannel channel, ILogger<RabbitMQConsumer> logger)
    {                       
        _configuration = configuration;
        _channel = channel;
        _connection = connection;
        _logger = logger;

        string host = _configuration["RabbitMQ_HostName"]!;
        int port = Convert.ToInt32(_configuration["RabbitMQ_Port"]!);
        string user = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;

        ConnectionFactory factory = new ConnectionFactory() { HostName = host, Port = port, UserName = user, Password = password};

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
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
        };

        await _channel.BasicConsumeAsync(queueName, true, consumer);
    }
} 