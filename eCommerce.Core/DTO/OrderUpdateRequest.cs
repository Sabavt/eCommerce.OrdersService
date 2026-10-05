namespace eCommerce.Core.DTO;

public record OrderUpdateRequest(
    Guid Id,
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemUpdateRequest> Items
)
{
    public OrderUpdateRequest() : this(Guid.Empty ,Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemUpdateRequest>()) { }
}