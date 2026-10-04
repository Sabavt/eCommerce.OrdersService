using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        RuleFor(o => o.Id)
            .NotNull().WithErrorCode("Id is required")
            .NotEmpty().WithErrorCode("Id can't be empty");

        RuleFor(o => o.Price)
            .NotNull().WithErrorCode("Price is required")
            .GreaterThan(0).WithErrorCode("Price must be a positive value");

        RuleFor(o => o.Quantity)
            .NotNull().WithErrorCode("Quantity is required")
            .GreaterThan(0).WithErrorCode("Quantity must be a positive value");

        RuleFor(o => o.ProductID)
            .NotNull().WithErrorCode("ProductID is required")
            .NotEmpty().WithErrorCode("ProductID can't be empty");
    }
} 