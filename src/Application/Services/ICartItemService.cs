using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartItemService
{
    Task<long> AddCartItemAsync(CartItemCreateDto dto);
    Task<CartItemGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<CartItemGetDto>> GetByCartIdAsync(long cartId);
    Task<IEnumerable<CartItemGetDto>> GetByUserIdAsync(long userId);
    Task UpdateAsync(long id, CartItemUpdateDto dto);
    Task IncrementQuantityAsync(long id, int amount = 1);
    Task DecrementQuantityAsync(long id, int amount = 1);
    Task ClearCartAsync(long cartId);
    Task<decimal> GetTotalPriceAsync(long cartId);
    Task DeleteAsync(long id);
}
