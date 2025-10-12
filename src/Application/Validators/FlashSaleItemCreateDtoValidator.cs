using FluentValidation;
using Application.DTOs.FlashSaleItems;

namespace Application.Validators;

public class FlashSaleItemCreateDtoValidator : AbstractValidator<FlashSaleItemCreateDto>
{
    public FlashSaleItemCreateDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than zero.");

        RuleFor(x => x.FlashSaleId)
            .GreaterThan(0).WithMessage("FlashSaleId must be greater than zero.");

        RuleFor(x => x.DiscountedPrice)
            .GreaterThan(0).WithMessage("Discounted price must be greater than zero.");
    }
}
