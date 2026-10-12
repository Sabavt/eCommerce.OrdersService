namespace eCommerce.Core.RabbitMQ.Messages;

public sealed record ProductDeletedMessage(int ProductID);