using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OrderGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<OrderGetDto> CreateAsync(OrderCreateDto dto, CancellationToken cancellationToken = default);
    Task<OrderGetDto?> UpdateAsync(OrderUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
