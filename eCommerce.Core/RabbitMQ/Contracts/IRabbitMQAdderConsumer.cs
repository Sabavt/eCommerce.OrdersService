namespace eCommerce.Core.RabbitMQ.Contracts;

public interface IRabbitMQAdderConsumer
{
    Task Consume();
} 