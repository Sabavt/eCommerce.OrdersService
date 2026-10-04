using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(o => o.UserID)
            .NotNull().WithErrorCode("UserID can't be null")
            .NotEmpty().WithErrorCode("User ID can't be empty");

        RuleFor(o => o.OrderDate)
           .NotNull().WithErrorCode("Order Date can't be null")
           .NotEmpty().WithErrorCode("Order Date can't be blank");

        RuleFor(o => o.Items)
            .NotNull().WithErrorCode("Items can't be null");
    }
} 