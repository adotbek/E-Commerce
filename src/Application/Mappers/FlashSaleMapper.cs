using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class FlashSaleMapper
{
    public static FlashSaleGetDto ToGetDto(FlashSale entity)
    {
        return new FlashSaleGetDto
        {
            Id = entity.Id,
            Name = entity.Name,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime
        };
    }

    public static FlashSale ToEntity(FlashSaleCreateDto dto)
    {
        return new FlashSale
        {
            Name = dto.Name,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime
        };
    }

    public static void UpdateEntity(FlashSale entity, FlashSaleUpdateDto dto)
    {
        entity.Name = dto.Name;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
    }
}
