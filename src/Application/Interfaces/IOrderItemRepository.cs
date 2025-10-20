using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(long id);
    Task<long> AddAsync(OrderItem entity);
    Task UpdateAsync(OrderItem entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(long orderId);
    Task<decimal> CalculateTotalAsync(long orderId);
    Task<bool> ExistsAsync(long orderItemId);
    Task<int> GetTotalQuantityAsync(long orderId);
    Task AddOrUpdateItemAsync(long orderId, long productId, int quantity);
    Task DeleteByOrderIdAsync(long orderId);
    Task<bool> ExistsInOrderAsync(long orderId, long productId);
}
