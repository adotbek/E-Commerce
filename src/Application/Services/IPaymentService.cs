using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentGetDto>> GetAllAsync();
    Task<PaymentGetDto?> GetByIdAsync(long id);
    Task<long> AddPaymentAsync(PaymentCreateDto dto);
    Task UpdateAsync(PaymentUpdateDto dto, long id);
    Task DeleteAsync(long id);
}
