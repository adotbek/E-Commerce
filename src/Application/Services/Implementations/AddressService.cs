using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.Mappers;

namespace Application.Services;

public class AddressService(IAddressRepository repository) : IAddressService
{
    public async Task<AddressGetDto> CreateAsync(AddressCreateDto dto)
    {
        var entity = AddressMapper.ToEntity(dto);
        var created = await repository.CreateAsync(entity);
        return AddressMapper.ToDto(created);
    }

    public async Task<ICollection<AddressGetDto>> GetByUserIdAsync(long userId)
    {
        var list = await repository.GetByUserIdAsync(userId);
        return list.Select(AddressMapper.ToDto).ToList();
    }

    public async Task<AddressGetDto?> GetByIdAsync(long id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity is null ? null : AddressMapper.ToDto(entity);
    }

    public async Task<AddressGetDto?> UpdateAsync(long id, AddressUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) return null;

        AddressMapper.UpdateEntity(entity, dto);
        var updated = await repository.UpdateAsync(entity);
        return AddressMapper.ToDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await repository.DeleteAsync(id);
    }
}
