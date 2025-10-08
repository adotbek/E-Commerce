using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IFlashSaleRepository
{
    Task<FlashSale> CreateAsync(FlashSale entity);
    Task<FlashSale?> GetByIdAsync(long id);
    Task<IEnumerable<FlashSale>> GetAllAsync();
    Task<IEnumerable<FlashSale>> GetActiveAsync(DateTime at);
    Task<FlashSale> UpdateAsync(FlashSale entity);
    Task<bool> DeleteAsync(long id);
}
