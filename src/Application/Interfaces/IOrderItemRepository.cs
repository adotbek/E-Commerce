using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OrderItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<OrderItem> AddAsync(OrderItem entity, CancellationToken cancellationToken = default);
    Task<OrderItem> UpdateAsync(OrderItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
