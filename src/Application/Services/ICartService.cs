using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartService
{
    Task<long> AddCartAsync(CartCreateDto dto);
    Task<CartGetDto?> GetByIdAsync(long id);
    Task<CartGetDto?> GetByUserIdAsync(long userId);
    Task UpdateAsync(long userId, CartUpdateDto dto);
    Task DeleteAsync(long id);
    Task<bool> ExistsByUserIdAsync(long userId);
    Task<decimal> CalculateTotalPriceAsync(long cartId);
    Task ClearCartAsync(long cartId);
}
