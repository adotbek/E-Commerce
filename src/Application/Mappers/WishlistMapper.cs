using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class WishlistMapper
{
    public static WishlistGetDto ToGetDto(Wishlist entity)
    {
        return new WishlistGetDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Items = entity.Items?.Select(WishlistItemMapper.ToDto).ToList()
        };
    }

    public static Wishlist ToEntity(WishlistCreateDto dto)
    {
        return new Wishlist
        {
            UserId = dto.UserId,
            Items = dto.ProductIds?.Select(id => new WishlistItem
            {
                ProductId = id
            }).ToList()
        };
    }

    public static void UpdateEntity(Wishlist entity, WishlistCreateDto dto)
    {
        entity.UserId = dto.UserId;

        entity.Items = dto.ProductIds?.Select(id => new WishlistItem
        {
            ProductId = id,
            WishlistId = entity.Id
        }).ToList();
    }
}
