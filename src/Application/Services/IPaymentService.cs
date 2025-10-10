using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaymentGetDto> AddAsync(PaymentCreateDto dto, CancellationToken cancellationToken = default);
    Task<PaymentGetDto> UpdateAsync(PaymentUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
