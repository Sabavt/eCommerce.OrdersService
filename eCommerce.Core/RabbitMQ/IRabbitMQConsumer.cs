namespace eCommerce.Core.RabbitMQ;

public interface IRabbitMQConsumer
{
    Task Consume();
}