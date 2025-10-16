using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemGetDto>> GetAllAsync();
    Task<OrderItemGetDto?> GetByIdAsync(long id);
    Task<long> AddOrderAsync(OrderItemCreateDto dto);
    Task UpdateAsync(OrderItemUpdateDto dto, long id);
    Task DeleteAsync(long id);
}
