using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class WishlistItemMapper
{
    public static WishlistItemGetDto ToDto(WishlistItem entity)
    {
        return new WishlistItemGetDto
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name ?? string.Empty,
            ProductPrice = entity.Product?.Price ?? 0
        };
    }

    public static WishlistItem ToEntity(WishlistItemGetDto dto)
    {
        return new WishlistItem
        {
            Id = dto.Id,
            ProductId = dto.ProductId
        };
    }

    public static void UpdateEntity(WishlistItem entity, WishlistItemGetDto dto)
    {
        entity.ProductId = dto.ProductId;
    }
}
