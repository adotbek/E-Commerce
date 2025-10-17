using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task<long> AddAsync(CartItem entity);
    Task<CartItem?> GetByIdAsync(long id);
    Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId);
    Task UpdateAsync(CartItem entity);
    Task DeleteAsync(long id);
     Task IncrementQuantityAsync(long id, int amount = 1);
    Task DecrementQuantityAsync(long id, int amount = 1);
    Task ClearCartAsync(long cartId);
    Task<decimal> GetTotalPriceAsync(long cartId);
}
