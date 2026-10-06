namespace eCommerce.Core.DTO;

public record OrderItemAddRequest(
    int ProductID,
    decimal? Price,
    int Quantity
)
{
    public OrderItemAddRequest() : this(0, null, 0) { }
}