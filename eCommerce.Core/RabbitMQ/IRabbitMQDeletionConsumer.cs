namespace eCommerce.Core.RabbitMQ
{
    public interface IRabbitMQDeletionConsumer
    {
        Task Consume();
        void Dispose(bool disposing);
    }
}