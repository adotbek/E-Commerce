using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentGetDto>> GetAllAsync();
    Task<PaymentGetDto?> GetByIdAsync(long id);
    Task<PaymentGetDto> CreateAsync(PaymentCreateDto dto);
    Task<PaymentGetDto> UpdateAsync(PaymentUpdateDto dto);
    Task DeleteAsync(long id);
}
