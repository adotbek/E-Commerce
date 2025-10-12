using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class AddressCreateDtoValidator : AbstractValidator<AddressCreateDto>
{
    public AddressCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City must not exceed 100 characters.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150).WithMessage("Street must not exceed 150 characters.");

        RuleFor(x => x.Apartment)
            .MaximumLength(50).WithMessage("Apartment must not exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Apartment));

        RuleFor(x => x.PostalCode)
            .Matches(@"^\d{4,10}$").WithMessage("Postal code must contain only digits (4–10 in length).")
            .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
    }
}
