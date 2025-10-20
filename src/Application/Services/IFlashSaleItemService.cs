namespace Application.Interfaces.Services;

using Application.DTOs.FlashSaleItems;
using Application.Dtos;

public interface IFlashSaleItemService
{
    Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync();
    Task<FlashSaleItemGetDto?> GetByIdAsync(long id);
    Task<long> AddFlashSaleItemService(FlashSaleItemCreateDto dto);
    Task UpdateAsync(FlashSaleItemUpdateDto dto, long id);
    Task DeleteAsync(long id);
    Task<IEnumerable<FlashSaleItemGetDto>> GetByFlashSaleIdAsync(long flashSaleId);
    Task<FlashSaleItemGetDto?> GetByProductIdAsync(long productId);
    Task<IEnumerable<FlashSaleItemGetDto>> GetActiveItemsAsync(DateTime now);
    Task<decimal> CalculateDiscountedPriceAsync(long productId, long flashSaleId);
    Task<bool> ExistsAsync(long productId, long flashSaleId);
    Task<int> RemoveExpiredItemsAsync(DateTime now);
}
