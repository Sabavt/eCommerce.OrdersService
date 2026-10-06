namespace eCommerce.Core.DTO;

public record OrderItemResponse(
    Guid Id,
    int ProductID,
    decimal Price,
    int Quantity,
    string ProductName,
    string Category
)
{
    public OrderItemResponse() : this(Guid.Empty, 0, 0m, 0, default!, default!) {  }
}