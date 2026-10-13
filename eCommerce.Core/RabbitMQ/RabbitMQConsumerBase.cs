using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace eCommerce.Core.RabbitMQ;

public class RabbitMQConsumerBase : IDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel = null!;
    private readonly IDistributedCache _cache = null!;
    private readonly IConfiguration _configuration;
    private bool disposedValue;

    public RabbitMQConsumerBase(IDistributedCache distributedCache, IConfiguration configuration)
    { 
        _cache = distributedCache;
        _configuration = configuration;

        string host = _configuration["RabbitMQ_HostName"]!;
        int port = Convert.ToInt32(_configuration["RabbitMQ_Port"]!);
        string user = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;

        ConnectionFactory factory = new ConnectionFactory() { HostName = host, Port = port, UserName = user, Password = password };

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    } 

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            { 
                _channel.Dispose();
                _connection?.Dispose();
            }
             
            disposedValue = true;
        }
    }

     
    ~RabbitMQConsumerBase()
    { 
        Dispose(disposing: false);
    }

    public void Dispose()
    { 
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
} 