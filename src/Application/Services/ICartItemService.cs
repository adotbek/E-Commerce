using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICartItemService
{
    Task<ICollection<CartItemDto>> GetUserCartAsync(long userId);
    Task AddToCartAsync(long userId, CartItemCreateDto dto);
    Task UpdateQuantityAsync(long userId, long cartItemId, int quantity);
    Task RemoveFromCartAsync(long userId, long cartItemId);
    Task<decimal> CalculateSubtotalAsync(long userId);
    Task ClearCartAsync(long userId);
}
