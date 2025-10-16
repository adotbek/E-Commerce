using Application.Dtos;
using Application.Common.Interfaces.Repositories;
using Application.Mappers;

namespace Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repository;

    public AddressService(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddAddressAsync(AddressCreateDto dto)
    {
        var entity = AddressMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<ICollection<AddressGetDto>> GetByUserIdAsync(long userId)
    {
        var addresses = await _repository.GetByUserIdAsync(userId);
        return addresses.Select(AddressMapper.ToDto).ToList();
    }

    public async Task<AddressGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : AddressMapper.ToDto(entity);
    }

    public async Task UpdateAsync(long id, AddressUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Address with ID {id} not found.");

        AddressMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
