namespace eCommerce.Core.DTO;

public record OrderAddRequest(
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemAddRequest> Items
)
{
    public OrderAddRequest() : this(Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemAddRequest>()) { }
}