using Application.Dtos;
using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(CategoryCreateDto dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }

    public static CategoryGetDto ToDto(Category entity)
    {
        return new CategoryGetDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static void UpdateEntity(Category entity, CategoryUpdateDto dto)
    {
        entity.Name = dto.Name;
    }
}
