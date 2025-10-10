using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Order> AddAsync(Order entity, CancellationToken cancellationToken = default);
    Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
