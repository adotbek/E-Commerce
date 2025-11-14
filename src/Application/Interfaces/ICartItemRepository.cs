using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task<ICollection<CartItem>> GetAllAsync();
    Task<CartItem?> GetByIdAsync(long id);
    Task AddAsync(CartItem item);
    Task UpdateAsync(CartItem item);
    Task DeleteAsync(long id);
    Task<ICollection<CartItem>> GetUserCartAsync(long userId);
    Task ClearUserCartAsync(long userId);
}
