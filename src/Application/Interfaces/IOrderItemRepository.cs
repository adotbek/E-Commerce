using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(long id);
    Task<OrderItem> AddAsync(OrderItem entity);
    Task<OrderItem> UpdateAsync(OrderItem entity);
    Task<bool> DeleteAsync(long id);
}
