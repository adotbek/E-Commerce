namespace Application.Mappers;

using Application.Dtos;
using Application.DTOs.FlashSaleItems;
using Domain.Entities;

public static class FlashSaleItemMapper
{
    public static FlashSaleItemGetDto ToDto(FlashSaleItem entity)
        => new()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            ProductName = entity.Product.Name,
            ProductImage = entity.Product.ImageUrl,
            OriginalPrice = entity.Product.Price,
            DiscountedPrice = entity.DiscountedPrice,
            FlashSaleId = entity.FlashSaleId
        };

    public static FlashSaleItem ToEntity(FlashSaleItemCreateDto dto)
        => new()
        {
            ProductId = dto.ProductId,
            DiscountedPrice = dto.DiscountedPrice,
            FlashSaleId = dto.FlashSaleId
        };

    public static void UpdateEntity(FlashSaleItem entity, FlashSaleItemGetDto dto)
    {
        entity.ProductId = dto.ProductId;
        entity.DiscountedPrice = dto.DiscountedPrice;
        entity.FlashSaleId = dto.FlashSaleId;
    }
}
