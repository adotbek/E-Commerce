using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class ConfirmCodeRequestDtoValidator : AbstractValidator<ConfirmCodeRequestDto>
{
    public ConfirmCodeRequestDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Confirmation code is required.")
            .Length(4, 8).WithMessage("Confirmation code must be between 4 and 8 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(320).WithMessage("Email must not exceed 320 characters.")
            .EmailAddress().WithMessage("Email format is invalid.");
    }
}
