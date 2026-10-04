namespace eCommerce.Core.DTO;

public record OrderItemResponse(
    Guid Id,
    Guid ProductID,
    decimal Price,
    int Quantity
)
{
    public OrderItemResponse() : this(Guid.Empty, Guid.Empty, 0m, 0) {  }
}