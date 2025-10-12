using FluentValidation;
using Application.Dtos;

namespace Application.Validators;

public class BannerCreateDtoValidator : AbstractValidator<BannerCreateDto>
{
    public BannerCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Subtitle)
            .MaximumLength(150).WithMessage("Subtitle must not exceed 150 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Subtitle));

        RuleFor(x => x.DiscountPercent)
            .InclusiveBetween(0, 100)
            .WithMessage("Discount percent must be between 0 and 100.")
            .When(x => x.DiscountPercent.HasValue);

        RuleFor(x => x.ImageUrl)
            .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
            .WithMessage("Image URL must be a valid absolute URL.")
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.LinkUrl)
            .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
            .WithMessage("Link URL must be a valid absolute URL.")
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));
    }
}
