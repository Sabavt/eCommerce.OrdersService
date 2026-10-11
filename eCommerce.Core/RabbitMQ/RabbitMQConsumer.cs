using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQConsumer : IRabbitMQConsumer, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMQConsumer(IConfiguration configuration, IConnection connection, IChannel channel)
    {                       
        _configuration = configuration;
        _channel = channel;
        _connection = connection;

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

    public void Consume()
    {
        string routingKey = "products.update.ProductName";
         
        _channel.ExchangeDeclareAsync(_configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, ExchangeType.Direct, true);

        _channel.QueueDeclareAsync("orders.product.update.ProductName.queue", true);
        _channel.QueueBindAsync("orders.product.update.ProductName.queue", _configuration["RabbitMQ_ProductsMicroservice_Exchange"]!, routingKey);
    }
}
