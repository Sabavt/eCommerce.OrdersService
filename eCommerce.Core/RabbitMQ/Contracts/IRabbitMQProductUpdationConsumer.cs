namespace eCommerce.Core.RabbitMQ.Contracts;

public interface IRabbitMQProductUpdationConsumer
{
    Task Consume(); 
}