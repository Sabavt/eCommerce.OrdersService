namespace eCommerce.Core.DTO;

public record OrderItemResponse(
    Guid Id,
    int ProductID,
    decimal Price,
    int Quantity
)
{
    public OrderItemResponse() : this(Guid.Empty, 0, 0m, 0) {  }
}