using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<IEnumerable<WishlistGetDto>> GetAllAsync();
    Task<WishlistGetDto?> GetByIdAsync(long id);
    Task<WishlistGetDto> CreateAsync(WishlistCreateDto dto);
    Task<bool> DeleteAsync(long id);
}
