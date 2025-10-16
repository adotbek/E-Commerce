using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IBannerService
{
    Task<long> AddBannerAsync(BannerCreateDto dto);
    Task<ICollection<BannerGetDto>> GetAllAsync();
    Task<BannerGetDto?> GetByIdAsync(long id);
    Task UpdateAsync(long id, BannerUpdateDto dto);
    Task DeleteAsync(long id);
}
