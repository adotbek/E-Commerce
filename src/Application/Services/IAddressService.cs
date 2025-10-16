using Application.Dtos;

namespace Application.Services;

public interface IAddressService
{
    Task <long> AddAddressAsync(AddressCreateDto dto);
    Task<ICollection<AddressGetDto>> GetByUserIdAsync(long userId);
    Task<AddressGetDto?> GetByIdAsync(long id);
    Task UpdateAsync(long id, AddressUpdateDto dto);
    Task DeleteAsync(long id);
}