using Application.Dtos;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product entity)
    {
        return new ProductDto
        {
            Id=entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
        };
    }

    public static Product ToEntity(this ProductCreateDto dto, long categoryId)
    {
        return new Product
        {
            CategoryId = categoryId,
            Name = dto.Name,
            Description = dto.Description,
            Brand = dto.Brand,
            Rating = dto.Rating,
            ImageUrl = dto.ImageUrl,
        };
    }

    public static void UpdateEntity(this Product entity, ProductDto dto, long categoryId)
    {
        entity.CategoryId = categoryId;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.ImageUrl = dto.ImageUrl;
    }
}
