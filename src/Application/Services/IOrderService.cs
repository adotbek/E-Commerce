using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderGetDto>> GetAllAsync();
    Task<OrderGetDto?> GetByIdAsync(long id);
    Task<OrderGetDto> CreateAsync(OrderCreateDto dto);
    Task<OrderGetDto?> UpdateAsync(OrderUpdateDto dto);
    Task DeleteAsync(long id);
}
