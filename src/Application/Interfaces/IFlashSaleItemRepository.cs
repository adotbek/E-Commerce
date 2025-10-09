namespace Application.Interfaces.Repositories;

using Domain.Entities;

public interface IFlashSaleItemRepository
{
    Task<IEnumerable<FlashSaleItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlashSaleItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task AddAsync(FlashSaleItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FlashSaleItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
