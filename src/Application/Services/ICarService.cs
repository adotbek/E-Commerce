using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartService
{
    Task<CartGetDto> CreateAsync(CartCreateDto dto);
    Task<CartGetDto?> GetByIdAsync(long id);
    Task<CartGetDto?> GetByUserIdAsync(long userId);
    Task<CartGetDto?> UpdateAsync(long id, CartUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}
