namespace Application.Mappers;

using Application.Dtos;
using Domain.Entities;

public static class FlashSaleItemMapper
{
    public static FlashSaleItemGetDto ToGetDto(FlashSaleItem entity)
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

    public static FlashSaleItem ToEntity(FlashSaleItemGetDto dto)
        => new()
        {
            Id = dto.Id,
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
