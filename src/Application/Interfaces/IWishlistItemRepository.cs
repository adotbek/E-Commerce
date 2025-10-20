using Application.Dtos;
using Domain.Entities;

namespace Domain.Repositories;

public interface IWishlistItemRepository
{
    Task<IEnumerable<WishlistItem>> GetAllAsync();
    Task<WishlistItem?> GetByIdAsync(long id);
    Task AddAsync(WishlistItem entity);
    Task UpdateAsync(WishlistItem entity);
    Task DeleteAsync(WishlistItem entity);
    Task<IEnumerable<WishlistItem>> GetByUserIdAsync(long userId);
    Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(long wishlistId);
    Task<bool> ExistsAsync(long wishlistId, long productId);
    Task<int> GetCountByWishlistIdAsync(long wishlistId);
    Task ClearWishlistAsync(long wishlistId);
}
