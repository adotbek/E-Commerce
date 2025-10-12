using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class PaymentCreateDtoValidator : AbstractValidator<PaymentCreateDto>
{
    public PaymentCreateDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId must be greater than zero.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Method)
            .NotEmpty().WithMessage("Payment method is required.")
            .MaximumLength(50).WithMessage("Payment method must not exceed 50 characters.");

        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("Transaction ID is required.")
            .MaximumLength(100).WithMessage("Transaction ID must not exceed 100 characters.");

        RuleFor(x => x.PaymentOptionId)
            .GreaterThan(0)
            .When(x => x.PaymentOptionId.HasValue)
            .WithMessage("PaymentOptionId must be greater than zero if provided.");
    }
}
