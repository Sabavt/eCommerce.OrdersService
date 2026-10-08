namespace eCommerce.Core.DTO;

public record OrderResponse(
    Guid Id,
    Guid UserID,
    DateTime OrderDate,
    decimal TotalAmount,
    List<OrderItemResponse> Items
)
{
    public string? PersonName { get; set; }
    public string? Email { get; set; }

    public OrderResponse() : this(Guid.Empty, Guid.Empty, DateTime.UtcNow, 0m, new List<OrderItemResponse>())  { }
} 