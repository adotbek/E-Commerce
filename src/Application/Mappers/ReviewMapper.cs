using Application.Dtos;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ReviewMapper
{
    public static ReviewDto ToDto(this Review entity)
    {
        return new ReviewDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProductId = entity.ProductId,
            Rating = entity.Rating,
            Comment = entity.Comment,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Review ToEntity(this ReviewDto dto)
    {
        return new Review
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedAt = dto.CreatedAt
        };
    }
}
