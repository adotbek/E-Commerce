using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;


public static class AddressMapper
{
    public static AddressGetDto ToDto(Address entity)
    {
        return new AddressGetDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Country = entity.Country,
            City = entity.City,
            Street = entity.Street,
            Apartment = entity.Apartment,
            PostalCode = entity.PostalCode
        };
    }

    public static Address ToEntity(AddressCreateDto dto)
    {
        return new Address
        {
            UserId = dto.UserId,
            Country = dto.Country,
            City = dto.City,
            Street = dto.Street,
            Apartment = dto.Apartment,
            PostalCode = dto.PostalCode
        };
    }

    public static void UpdateEntity(Address entity, AddressUpdateDto dto)
    {
        entity.Country = dto.Country;
        entity.City = dto.City;
        entity.Street = dto.Street;
        entity.Apartment = dto.Apartment;
        entity.PostalCode = dto.PostalCode;
    }
}
