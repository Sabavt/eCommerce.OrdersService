namespace eCommerce.Core.RabbitMQ.Messages;

public sealed record ProductUpdationMessage(string ProductName, int ProductID); 