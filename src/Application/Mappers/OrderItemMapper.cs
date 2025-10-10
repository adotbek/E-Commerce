using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class OrderItemMapper
{
    public static OrderItemGetDto ToGetDto(OrderItem entity)
    {
        return new OrderItemGetDto
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name ?? string.Empty,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice
        };
    }

    public static OrderItem ToEntity(OrderItemCreateDto dto)
    {
        return new OrderItem
        {
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice
        };
    }

    public static void UpdateEntity(OrderItem entity, OrderItemUpdateDto dto)
    {
        entity.Quantity = dto.Quantity;
        entity.UnitPrice = dto.UnitPrice;
    }
}
