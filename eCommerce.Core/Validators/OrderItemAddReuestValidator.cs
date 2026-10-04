using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
    public OrderItemAddRequestValidator()
    {
        RuleFor(o => o.ProductID)
             .NotNull().WithErrorCode("ProductID is required")
             .NotEmpty().WithErrorCode("ProductID can't be empty");

        RuleFor(o => o.Quantity)
            .NotNull().WithErrorCode("Quantity is required")
            .NotEqual(0).WithErrorCode("Quantity can't be zero");

        RuleFor(o => o.Price)
            .NotNull().WithErrorCode("Price is required")
            .GreaterThan(0).WithErrorCode("Price must be a positive value");
    }
}