using eCommerce.Core.RabbitMQ.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQDeletionConsumer : IDisposable, IRabbitMQDeletionConsumer
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly ILogger<RabbitMQDeletionConsumer> _logger;
    private bool _disposed = false;

    public RabbitMQDeletionConsumer(IConfiguration configuration, IConnection connection, IChannel channel, ILogger<RabbitMQDeletionConsumer> logger)
    {
        _configuration = configuration;
        _channel = channel;
        _connection = connection;
        _logger = logger;

        string host = _configuration["RabbitMQ_HostName"]!;
        int port = Convert.ToInt32(_configuration["RabbitMQ_Port"]!);
        string user = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;

        ConnectionFactory factory = new ConnectionFactory() { HostName = host, Port = port, UserName = user, Password = password };

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }


    public void Dispose(bool disposing)
    {
        if (_disposed is false)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _connection.Dispose();
                _channel.Dispose();
            }

            _disposed = true;
        }
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
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            var deleted_product = JsonSerializer.Deserialize<ProductDeletedMessage>(message);

            _logger.LogInformation($"product deleted - {deleted_product?.ProductID}");
        };

        await _channel.BasicConsumeAsync(queueName, true, consumer);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RabbitMQDeletionConsumer()
    {
        Dispose(false);
    }
}