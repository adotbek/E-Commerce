using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderGetDto>> GetAllAsync();
    Task<OrderGetDto?> GetByIdAsync(long id);
    Task<long> AddOrderAsync(OrderCreateDto dto);
    Task UpdateAsync(OrderUpdateDto dto, long id);
    Task DeleteAsync(long id);

    Task<IEnumerable<OrderGetDto>> GetByUserIdAsync(long userId);
    Task UpdateStatusAsync(long id, string status);
    Task<IEnumerable<OrderGetDto>> GetByStatusAsync(string status);

    Task<decimal> CalculateTotalAmountAsync(long orderId);
    Task<IEnumerable<OrderGetDto>> GetRecentOrdersAsync(int count);
    Task<bool> ExistsAsync(long orderId);
    Task<IEnumerable<OrderGetDto>> GetPendingOrdersAsync();
    Task<IEnumerable<OrderGetDto>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<int> GetTotalOrdersCountAsync();
}
