using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IFlashSaleRepository
{
    Task<long> AddAsync(FlashSale entity);
    Task<FlashSale?> GetByIdAsync(long id);
    Task<IEnumerable<FlashSale>> GetAllAsync();
    Task<IEnumerable<FlashSale>> GetActiveAsync(DateTime at);
    Task UpdateAsync(FlashSale entity);
    Task DeleteAsync(long id);
}
