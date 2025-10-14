namespace Application.Interfaces.Repositories;

using Domain.Entities;

public interface IFlashSaleItemRepository
{
    Task<IEnumerable<FlashSaleItem>> GetAllAsync();
    Task<FlashSaleItem?> GetByIdAsync(long id);
    Task <long> AddAsync(FlashSaleItem entity);
    Task UpdateAsync(FlashSaleItem entity);
    Task DeleteAsync(long id);
}
