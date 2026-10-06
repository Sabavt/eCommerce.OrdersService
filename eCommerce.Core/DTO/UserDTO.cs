namespace eCommerce.Core.DTO;

public record UserDTO(Guid UserID, string? Email, string? PersonName, string? Gender)
{
    public UserDTO() : this(Guid.Empty, string.Empty, string.Empty, string.Empty) { }
}