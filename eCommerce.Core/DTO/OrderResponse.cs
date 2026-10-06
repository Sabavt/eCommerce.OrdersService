namespace eCommerce.Core.DTO;

public record OrderResponse(
    Guid Id,
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemResponse> Items,
    string? PersonName = null,
    string? Email = null
)
{
    public OrderResponse() : this(Guid.Empty, Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemResponse>(), null, null)  { }
} 