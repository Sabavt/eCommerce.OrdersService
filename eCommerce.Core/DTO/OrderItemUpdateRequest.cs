namespace eCommerce.Core.DTO;

public record OrderItemUpdateRequest(
    Guid Id,
    int ProductID,
    decimal? Price,
    int Quantity
)
{
    public OrderItemUpdateRequest() : this(Guid.Empty, 0, null, 0) { }
} 