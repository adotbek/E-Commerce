using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class BannerMapper
{
    public static BannerGetDto ToGetDto(Banner entity)
    {
        return new BannerGetDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Subtitle = entity.Subtitle,
            DiscountPercent = entity.DiscountPercent,
            ImageUrl = entity.ImageUrl,
            LinkUrl = entity.LinkUrl,
            IsActive = entity.IsActive
        };
    }

    public static Banner ToEntity(BannerCreateDto dto)
    {
        return new Banner
        {
            Title = dto.Title,
            Subtitle = dto.Subtitle,
            DiscountPercent = dto.DiscountPercent,
            ImageUrl = dto.ImageUrl,
            LinkUrl = dto.LinkUrl,
            IsActive = true
        };
    }

    public static void UpdateEntity(Banner entity, BannerUpdateDto dto)
    {
        entity.Title = dto.Title;
        entity.Subtitle = dto.Subtitle;
        entity.DiscountPercent = dto.DiscountPercent;
        entity.ImageUrl = dto.ImageUrl;
        entity.LinkUrl = dto.LinkUrl;
        entity.IsActive = dto.IsActive;
    }
}
