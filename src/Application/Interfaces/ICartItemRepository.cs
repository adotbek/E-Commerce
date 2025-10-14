using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartItemRepository
{
    Task<long> AddAsync(CartItem entity);
    Task<CartItem?> GetByIdAsync(long id);
    Task<IEnumerable<CartItem>> GetByCartIdAsync(long cartId);
    Task UpdateAsync(CartItem entity);
    Task DeleteAsync(long id);
}
