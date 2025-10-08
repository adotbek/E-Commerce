using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartItemService
{
    Task<CartItemGetDto> CreateAsync(CartItemCreateDto dto);
    Task<CartItemGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<CartItemGetDto>> GetByCartIdAsync(long cartId);
    Task<CartItemGetDto?> UpdateAsync(long id, CartItemUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}
