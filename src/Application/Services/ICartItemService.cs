using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartItemService
{
    Task<long> AddCartItemAsync(CartItemCreateDto dto);
    Task<CartItemGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<CartItemGetDto>> GetByCartIdAsync(long cartId);
    Task UpdateAsync(long id, CartItemUpdateDto dto);
    Task DeleteAsync(long id);
}
