using Application.Dtos;

namespace Application.Services;

public interface IAddressService
{
    Task<AddressGetDto> CreateAsync(AddressCreateDto dto);
    Task<ICollection<AddressGetDto>> GetByUserIdAsync(long userId);
    Task<AddressGetDto?> GetByIdAsync(long id);
    Task<AddressGetDto?> UpdateAsync(long id, AddressUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}