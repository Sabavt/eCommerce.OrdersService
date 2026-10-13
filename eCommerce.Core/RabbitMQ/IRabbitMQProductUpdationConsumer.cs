namespace eCommerce.Core.RabbitMQ;

public interface IRabbitMQProductUpdationConsumer
{
    Task Consume(); 
}