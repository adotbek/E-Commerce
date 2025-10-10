using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentOptionService
{
    Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentOptionGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaymentOptionGetDto> AddAsync(PaymentOptionCreateDto dto, CancellationToken cancellationToken = default);
    Task<PaymentOptionGetDto> UpdateAsync(PaymentOptionUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
