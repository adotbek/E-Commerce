using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductImageMapper
{
    public static ProductImageDto ToDto(this ProductImage entity)
    {
        return new ProductImageDto
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            ImageUrl = entity.ImageUrl
        };
    }

    public static ProductImage ToEntity(this ProductImageDto dto)
    {
        return new ProductImage
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            ImageUrl = dto.ImageUrl
        };
    }
}
