using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistItemService
{
    Task<IEnumerable<WishlistItemGetDto>> GetAllAsync();
    Task<WishlistItemGetDto?> GetByIdAsync(long id);
    Task<long> AddWishlistItemAsync(WishlistItemGetDto dto);
    Task UpdateAsync(long id, WishlistItemGetDto dto);
    Task DeleteAsync(long id);
    Task<IEnumerable<WishlistItemGetDto>> GetByUserIdAsync(long userId);
    Task<IEnumerable<WishlistItemGetDto>> GetByWishlistIdAsync(long wishlistId);
    Task<bool> ExistsAsync(long wishlistId, long productId);
    Task<int> GetCountByWishlistIdAsync(long wishlistId);
    Task ClearWishlistAsync(long wishlistId);
}
