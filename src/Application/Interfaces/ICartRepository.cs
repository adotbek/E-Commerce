using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<long> AddAsync(Cart entity);
    Task<Cart?> GetByIdAsync(long id);
    Task<Cart?> GetByUserIdAsync(long userId);
    Task UpdateAsync(Cart entity);
    Task DeleteAsync(long id);
    Task<bool> ExistsByUserIdAsync(long userId);
    Task<decimal> CalculateTotalPriceAsync(long cartId);
    Task ClearCartAsync(long cartId);
}
