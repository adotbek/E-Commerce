using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderMapper
{
    public static OrderGetDto ToDto(Order entity)
    {
        return new OrderGetDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            TotalAmount = entity.TotalAmount,
            ShippingAddress = entity.ShippingAddress,
            PaymentMethod = entity.PaymentMethod,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            Items = entity.Items?.Select(OrderItemMapper.ToDto).ToList()
        };
    }

    public static Order ToEntity(OrderCreateDto dto)
    {
        return new Order
        {
            UserId = dto.UserId,
            TotalAmount = dto.TotalAmount,
            ShippingAddress = dto.ShippingAddress,
            PaymentMethod = dto.PaymentMethod,
            Items = dto.Items?.Select(OrderItemMapper.ToEntity).ToList()
        };
    }

    public static void UpdateEntity(Order entity, OrderUpdateDto dto)
    {
        if (dto.ShippingAddress is not null)
            entity.ShippingAddress = dto.ShippingAddress;

        if (dto.PaymentMethod is not null)
            entity.PaymentMethod = dto.PaymentMethod;

        if (dto.Status is not null)
            entity.Status = dto.Status;
    }
}
