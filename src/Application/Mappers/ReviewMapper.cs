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

    public static void UpdateEntity(this Review entity, ReviewDto dto)
    {
        entity.UserId = dto.UserId;
        entity.ProductId = dto.ProductId;
        entity.Rating = dto.Rating;
        entity.Comment = dto.Comment;
        entity.CreatedAt = dto.CreatedAt;
    }
}
