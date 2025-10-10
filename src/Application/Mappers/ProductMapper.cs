using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            DiscountPrice = entity.DiscountPrice,
            StockQuantity = entity.StockQuantity,
            Brand = entity.Brand,
            Rating = entity.Rating,
            ReviewCount = entity.ReviewCount,
            ImageUrl = entity.ImageUrl,
            IsFeatured = entity.IsFeatured,
            IsNewArrival = entity.IsNewArrival
        };
    }

    public static Product ToEntity(this ProductDto dto, long categoryId)
    {
        return new Product
        {
            Id = dto.Id,
            CategoryId = categoryId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            StockQuantity = dto.StockQuantity,
            Brand = dto.Brand,
            Rating = dto.Rating,
            ReviewCount = dto.ReviewCount,
            ImageUrl = dto.ImageUrl,
            IsFeatured = dto.IsFeatured,
            IsNewArrival = dto.IsNewArrival
        };
    }
}
