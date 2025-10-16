using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<long> AddWishlistAsync(WishlistCreateDto dto);
    Task<IEnumerable<WishlistGetDto>> GetAllAsync();
    Task<WishlistGetDto?> GetByIdAsync(long id);
    Task UpdateAsync(WishlistCreateDto dto, long id);
    Task DeleteAsync(long id);
}
