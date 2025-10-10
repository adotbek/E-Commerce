using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OrderItemGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<OrderItemGetDto> AddAsync(OrderItemCreateDto dto, CancellationToken cancellationToken = default);
    Task<OrderItemGetDto> UpdateAsync(OrderItemUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
