using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderMapper
{
    public static OrderGetDto ToDto(Order entity)
    {
        //return new OrderGetDto
        //{
        //    Id = entity.Id,
        //    UserId = entity.UserId,
        //    TotalPrice = entity.TotalPrice,
        //    ShippingAddress = entity.ShippingAddress,
        //    PaymentMethod = entity.PaymentMethod,
        //    Status = entity.Status.ToString(),
        //    CreatedAt = entity.CreatedAt,
        //    Items = entity.Items?.Select(OrderItemMapper.ToDto).ToList()
        //};

        throw new NotImplementedException();
    }

    public static Order ToEntity(OrderCreateDto dto)
    {
        //return new Order
        //{
        //    UserId = dto.UserId,
        //    TotalPrice = dto.TotalPrice,
        //    ShippingAddress = dto.ShippingAddress,
        //    PaymentMethod = dto.PaymentMethod,
        //    Items = dto.Items?.Select(OrderItemMapper.ToEntity).ToList()
        //};
        throw new NotImplementedException();
    }

    public static void UpdateEntity(Order entity, OrderUpdateDto dto)
    {
        throw new NotImplementedException();
        //if (dto.ShippingAddress is not null)
        //    entity.ShippingAddress = dto.ShippingAddress;

        //if (dto.PaymentMethod is not null)
        //    entity.PaymentMethod = dto.PaymentMethod;

        //    entity.Status = dto.Status;
    }
}
