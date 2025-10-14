using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(long id);
    Task<long> AddAsync(OrderItem entity);
    Task UpdateAsync(OrderItem entity);
    Task DeleteAsync(long id);
}
