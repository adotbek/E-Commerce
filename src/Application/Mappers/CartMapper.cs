using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class CartMapper
{
    public static CartGetDto ToGetDto(Cart entity)
    {
        return new CartGetDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            TotalPrice = entity.TotalPrice
        };
    }

    public static Cart ToEntity(CartCreateDto dto)
    {
        return new Cart
        {
            UserId = dto.UserId,
            TotalPrice = 0
        };
    }

    public static void UpdateEntity(Cart entity, CartUpdateDto dto)
    {
        entity.TotalPrice = dto.TotalPrice;
    }
}
