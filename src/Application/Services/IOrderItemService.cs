using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemGetDto>> GetAllAsync();
    Task<OrderItemGetDto?> GetByIdAsync(long id);
    Task<OrderItemGetDto> CreateAsync(OrderItemCreateDto dto);
    Task<OrderItemGetDto> UpdateAsync(OrderItemUpdateDto dto);
    Task DeleteAsync(long id);
}
