namespace eCommerce.Core.DTO;

public record OrderItemResponse(
    Guid Id,
    int ProductID,
    decimal Price
)
{
    public int Quantity {  get; set; }
    public string? ProductName { get; set; } 
    public string? Category { get; set; }

    public OrderItemResponse() : this(Guid.Empty, 0, 0m) {  }
}