using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<Order> AddAsync(Order entity);
    Task<Order> UpdateAsync(Order entity);
    Task<bool> DeleteAsync(long id);
}
