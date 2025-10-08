using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IFlashSaleService
{
    Task<FlashSaleGetDto> CreateAsync(FlashSaleCreateDto dto);
    Task<FlashSaleGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<FlashSaleGetDto>> GetAllAsync();
    Task<IEnumerable<FlashSaleGetDto>> GetActiveAsync(DateTime? at = null);
    Task<FlashSaleGetDto?> UpdateAsync(long id, FlashSaleUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}
