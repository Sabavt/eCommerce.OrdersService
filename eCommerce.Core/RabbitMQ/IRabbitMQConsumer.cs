namespace eCommerce.Core.RabbitMQ;

public interface IRabbitMQConsumer
{
    Task Consume();
    void Dispose(bool disposing);
}