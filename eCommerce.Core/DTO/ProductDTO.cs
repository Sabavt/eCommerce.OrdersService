namespace eCommerce.Core.DTO;

public record ProductDTO(int? ProductID,string? ProductName, int Quantity, string? ProductDesciption, decimal Price, string? Category)
{
    public ProductDTO() : this(null,default, default, default, default, default) { }
} 