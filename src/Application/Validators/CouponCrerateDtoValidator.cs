using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class CouponCreateDtoValidator : AbstractValidator<CouponCreateDto>
{
    public CouponCreateDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code is required.")
            .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.");

        RuleFor(x => x.DiscountPercent)
            .InclusiveBetween(0.1, 100)
            .WithMessage("Discount percent must be between 0.1 and 100.");

        RuleFor(x => x.ValidUntil)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Valid until date must be in the future.");
    }
}
