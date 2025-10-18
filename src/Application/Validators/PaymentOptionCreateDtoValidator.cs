using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class PaymentOptionCreateDtoValidator : AbstractValidator<PaymentOptionCreateDto>
{
    public PaymentOptionCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.CardHolderName)
            .NotEmpty().WithMessage("Card holder name is required.")
            .MaximumLength(100).WithMessage("Card holder name must not exceed 100 characters.");

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .Matches(@"^\d{13,19}$").WithMessage("Card number must be between 13 and 19 digits.");

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(0).WithMessage("Expiry date must be a valid positive number.");

        RuleFor(x => x.ExpiryMonth)
    .InclusiveBetween(1, 12)
    .WithMessage("Expire month must be between 1 and 12.");

        RuleFor(x => x.ExpiryYear)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage("Expire year must not be in the past.");

        RuleFor(x => x.CardType)
            .NotEmpty().WithMessage("Card type is required.")
            .MaximumLength(50).WithMessage("Card type must not exceed 50 characters.");
    }
}
