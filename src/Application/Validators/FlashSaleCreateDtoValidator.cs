using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class FlashSaleCreateDtoValidator : AbstractValidator<FlashSaleCreateDto>
{
    public FlashSaleCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Flash sale name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be later than start time.");
    }
}
