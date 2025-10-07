using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<Cart> CreateAsync(Cart entity);
    Task<Cart?> GetByIdAsync(long id);
    Task<Cart?> GetByUserIdAsync(long userId);
    Task<Cart> UpdateAsync(Cart entity);
    Task<bool> DeleteAsync(long id);
}
