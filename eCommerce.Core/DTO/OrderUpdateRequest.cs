namespace eCommerce.Core.DTO;

public record OrderUpdateReuest(
    Guid Id,
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemAddRequest> Items
)
{
    public OrderUpdateReuest() : this(Guid.Empty ,Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemAddRequest>()) { }
}