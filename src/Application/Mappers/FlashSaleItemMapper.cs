using Application.Dtos;
using Application.DTOs.FlashSaleItems;
using Domain.Entities;

namespace Application.Mappers;

public static class FlashSaleItemMapper
{
    public static FlashSaleItemGetDto ToGetDto(FlashSaleItem entity, string productName, string productImage, decimal originalPrice)
    {
        var discountPercent = (int)Math.Round((1 - (entity.DiscountedPrice / originalPrice)) * 100);

        return new FlashSaleItemGetDto
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            FlashSaleId = entity.FlashSaleId,
            DiscountedPrice = entity.DiscountedPrice,
            ProductName = productName,
            ProductImage = productImage,
            OriginalPrice = originalPrice,
            DiscountPercent = discountPercent
        };
    }

    public static FlashSaleItem ToEntity(FlashSaleItemCreateDto dto)
    {
        return new FlashSaleItem
        {
            ProductId = dto.ProductId,
            FlashSaleId = dto.FlashSaleId,
            DiscountedPrice = dto.DiscountedPrice
        };
    }

    public static void UpdateEntity(FlashSaleItem entity, FlashSaleItemUpdateDto dto)
    {
        entity.DiscountedPrice = dto.DiscountedPrice;
    }
}
