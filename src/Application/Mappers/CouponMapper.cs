using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class CouponMapper
{
    public static CouponGetDto ToDto(Coupon entity)
    {
        return new CouponGetDto
        {
            Id = entity.Id,
            Code = entity.Code,
            DiscountPercent = entity.DiscountPercent,
            IsActive = entity.IsActive,
            ValidUntil = entity.ValidUntil
        };
    }

    public static Coupon ToEntity(CouponCreateDto dto)
    {
        return new Coupon
        {
            Code = dto.Code,
            DiscountPercent = dto.DiscountPercent,
            ValidUntil = dto.ValidUntil
        };
    }

    public static void UpdateEntity(Coupon entity, CouponUpdateDto dto)
    {
        entity.DiscountPercent = dto.DiscountPercent;
        entity.IsActive = dto.IsActive;
        entity.ValidUntil = dto.ValidUntil;
    }
}
