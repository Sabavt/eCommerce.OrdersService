namespace eCommerce.Core.DTO;

public record OrderUpdateRequest(
    Guid Id,
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemAddRequest> Items
)
{
    public OrderUpdateRequest() : this(Guid.Empty ,Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemAddRequest>()) { }
}