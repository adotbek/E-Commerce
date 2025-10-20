using Application.Dtos;
using Domain.Entities;

namespace Domain.Repositories;

public interface IWishlistRepository
{
    Task<IEnumerable<Wishlist>> GetAllAsync();
    Task<Wishlist?> GetByIdAsync(long id);
    Task AddAsync(Wishlist entity);
    Task UpdateAsync(Wishlist entity);
    Task DeleteAsync(Wishlist entity);
    Task<Wishlist?> GetByUserIdAsync(long userId);
    Task<bool> ExistsByUserIdAsync(long userId);
    Task<int> GetItemCountAsync(long wishlistId);
    Task ClearAsync(long wishlistId);
}
