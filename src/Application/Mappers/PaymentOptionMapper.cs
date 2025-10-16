using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class PaymentOptionMapper
{
    public static PaymentOptionGetDto ToDto(PaymentOption entity)
    {
        return new PaymentOptionGetDto
        {
            Id = entity.Id,
            CardHolderName = entity.CardHolderName,
            CardNumberMasked = MaskCardNumber(entity.CardNumber),
            ExpiryDate = entity.ExpiryDate,
            CardType = entity.CardType
        };
    }

    public static PaymentOption ToEntity(PaymentOptionCreateDto dto)
    {
        return new PaymentOption
        {
            UserId = dto.UserId,
            CardHolderName = dto.CardHolderName,
            CardNumber = dto.CardNumber,
            ExpiryDate = dto.ExpiryDate,
            CardType = dto.CardType
        };
    }

    public static void UpdateEntity(PaymentOption entity, PaymentOptionUpdateDto dto)
    {
        entity.CardHolderName = dto.CardHolderName;
        entity.ExpiryDate = dto.ExpiryDate;
    }

    private static string MaskCardNumber(string cardNumber)
    {
        if (cardNumber.Length < 4)
            return "****";
        return $"**** **** **** {cardNumber[^4..]}";
    }
}
