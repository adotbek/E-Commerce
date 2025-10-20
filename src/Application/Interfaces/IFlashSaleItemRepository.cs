namespace Application.Interfaces.Repositories;

using Application.Dtos;
using Domain.Entities;

public interface IFlashSaleItemRepository
{
    Task<IEnumerable<FlashSaleItem>> GetAllAsync();
    Task<FlashSaleItem?> GetByIdAsync(long id);
    Task <long> AddAsync(FlashSaleItem entity);
    Task UpdateAsync(FlashSaleItem entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<FlashSaleItem>> GetByFlashSaleIdAsync(long flashSaleId);
    Task<FlashSaleItem?> GetByProductIdAsync(long productId);
    Task<IEnumerable<FlashSaleItem>> GetActiveItemsAsync(DateTime now);
    Task<decimal> CalculateDiscountedPriceAsync(long productId, long flashSaleId);
    Task<bool> ExistsAsync(long productId, long flashSaleId);
    Task<int> RemoveExpiredItemsAsync(DateTime now);
}
