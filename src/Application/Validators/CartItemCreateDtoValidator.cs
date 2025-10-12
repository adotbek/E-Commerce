using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class CartItemCreateDtoValidator : AbstractValidator<CartItemCreateDto>
{
    public CartItemCreateDtoValidator()
    {
        RuleFor(x => x.CartId)
            .GreaterThan(0).WithMessage("CartId must be greater than zero.");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(100).WithMessage("Quantity cannot exceed 100 items.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
    }
}
