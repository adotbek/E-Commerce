using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<long> AddWishlistAsync(WishlistCreateDto dto);
    Task<IEnumerable<WishlistGetDto>> GetAllAsync();
    Task<WishlistGetDto?> GetByIdAsync(long id);
    Task UpdateAsync(WishlistCreateDto dto, long id);
    Task DeleteAsync(long id);
    Task<WishlistGetDto?> GetByUserIdAsync(long userId);
    Task<bool> ExistsByUserIdAsync(long userId);
    Task<int> GetItemCountAsync(long wishlistId);
    Task ClearAsync(long wishlistId);
}
