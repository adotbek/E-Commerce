using Domain.Entities;

namespace Domain.Repositories;

public interface IWishlistItemRepository
{
    Task<IEnumerable<WishlistItem>> GetAllAsync();
    Task<WishlistItem?> GetByIdAsync(long id);
    Task AddAsync(WishlistItem entity);
    Task UpdateAsync(WishlistItem entity);
    Task DeleteAsync(WishlistItem entity);
    Task SaveChangesAsync();
}
