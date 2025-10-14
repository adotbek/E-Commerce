using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<long> AddAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(long id);
}
