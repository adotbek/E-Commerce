using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderGetDto>> GetAllAsync();
    Task<OrderGetDto?> GetByIdAsync(long id);
    Task<long> AddOrderAsync(OrderCreateDto dto);
    Task UpdateAsync(OrderUpdateDto dto);
    Task DeleteAsync(long id);
}
