using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(o => o.UserID)
            .NotNull().WithMessage("UserID can't be null"); 
    }
} 