using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.Core.Domain.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderID { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserID { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public DateTime OrderDate { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
} 