using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IFlashSaleService
{
    Task<long> AddFlashSaleAsync(FlashSaleCreateDto dto);
    Task<FlashSaleGetDto?> GetByIdAsync(long id);
    Task<IEnumerable<FlashSaleGetDto>> GetAllAsync();
    Task<IEnumerable<FlashSaleGetDto>> GetActiveAsync(DateTime? at = null);
    Task UpdateAsync(long id, FlashSaleUpdateDto dto);
    Task DeleteAsync(long id);
}
    