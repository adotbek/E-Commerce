using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task<CartItem> CreateAsync(CartItem entity);
    Task<CartItem?> GetByIdAsync(long id);
    Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId);
    Task<CartItem> UpdateAsync(CartItem entity);
    Task<bool> DeleteAsync(long id);
}
