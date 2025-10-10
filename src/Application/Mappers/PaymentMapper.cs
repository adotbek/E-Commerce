using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class PaymentMapper
{
    public static PaymentGetDto ToGetDto(this Payment entity)
    {
        return new PaymentGetDto
        {
            Id = entity.Id,
            OrderId = entity.OrderId,
            Amount = entity.Amount,
            Method = entity.Method,
            TransactionId = entity.TransactionId,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Payment ToEntity(this PaymentCreateDto dto)
    {
        return new Payment
        {
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            Method = dto.Method,
            TransactionId = dto.TransactionId,
            PaymentOptionId = dto.PaymentOptionId
        };
    }

    public static void UpdateEntity(this Payment entity, PaymentUpdateDto dto)
    {
        entity.Status = dto.Status;
    }
}
