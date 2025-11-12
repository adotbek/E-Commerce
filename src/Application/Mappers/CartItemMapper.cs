using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class CartItemMapper
{
    public static CartItemGetDto ToDto(CartItem entity)
    {
        return new CartItemGetDto
        {
            Id = entity.Id,
            CartId = entity.CartId,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice
        };
    }

    public static CartItem ToEntity(CartItemCreateDto dto)
    {
        return new CartItem
        {
            CartId = dto.CartId,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice
        };
    }

    public static void UpdateEntity(CartItem entity, CartItemUpdateDto dto)
    {
        entity.Quantity = dto.Quantity;
        entity.UnitPrice = dto.UnitPrice;
    }
}
