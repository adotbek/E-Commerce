using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistItemService
{
    Task<IEnumerable<WishlistItemGetDto>> GetAllAsync();
    Task<WishlistItemGetDto?> GetByIdAsync(long id);
    Task<WishlistItemGetDto> CreateAsync(WishlistItemGetDto dto);
    Task<WishlistItemGetDto?> UpdateAsync(long id, WishlistItemGetDto dto);
    Task<bool> DeleteAsync(long id);
}
