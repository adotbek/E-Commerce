using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentOptionService
{
    Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync();
    Task<PaymentOptionGetDto?> GetByIdAsync(long id);
    Task<long> AddPaymentOptionAsync(PaymentOptionCreateDto dto);
    Task UpdateAsync(PaymentOptionUpdateDto dto);
    Task DeleteAsync(long id);
}
