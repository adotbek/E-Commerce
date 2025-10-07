using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IBannerService
{
    Task<BannerGetDto> CreateAsync(BannerCreateDto dto);
    Task<ICollection<BannerGetDto>> GetAllAsync();
    Task<BannerGetDto?> GetByIdAsync(long id);
    Task<BannerGetDto?> UpdateAsync(long id, BannerUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}
