namespace eCommerce.Core.RabbitMQ.Contracts
{
    public interface IRabbitMQDeletionConsumer
    {
        Task Consume(); 
    }
}