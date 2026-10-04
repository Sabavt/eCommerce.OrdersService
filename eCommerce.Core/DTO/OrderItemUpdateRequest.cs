namespace eCommerce.Core.DTO;

public record OrderItemUpdateRequest(
    Guid Id,
    Guid ProductID,
    decimal? Price,
    int Quantity
)
{
    public OrderItemUpdateRequest() : this(Guid.Empty, Guid.Empty, null, 0) { }
} 