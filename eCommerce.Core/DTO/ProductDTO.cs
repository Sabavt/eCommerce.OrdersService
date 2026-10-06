namespace eCommerce.Core.DTO;

public record ProductDTO(string? ProductName, int Quantity, string? ProductDesciption, decimal Price, CategoryOptions Category)
{
    public ProductDTO() : this(default, default, default, default, default) { }
} 