using Application.Dtos;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<IEnumerable<WishlistGetDto>> GetAllAsync();
    Task<long?> GetByIdAsync(long id);
    Task AddWishlistAsync(WishlistCreateDto dto, long id);
    Task DeleteAsync(long id);
}
