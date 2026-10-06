using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.Core.Domain.Entities;

public class OrderItem
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
    public int ProductID { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public decimal? Price { get; set; } 
    [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
    public int Quantity { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public decimal? Total => Price * Quantity;
}