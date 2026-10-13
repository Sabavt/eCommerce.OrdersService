namespace eCommerce.Core.RabbitMQ
{
    public interface IRabbitMQDeletionConsumer
    {
        Task Consume(); 
    }
}