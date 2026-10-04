namespace eCommerce.Core.DTO;

public record OrderItemAddRequest(
    Guid ProductID,
    decimal? Price,
    int Quantity
)
{
    public OrderItemAddRequest() : this(Guid.Empty, null, 0)
    {
        
    }
}