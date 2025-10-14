using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentOptionService
{
    Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync();
    Task<PaymentOptionGetDto?> GetByIdAsync(long id);
    Task<PaymentOptionGetDto> CreateAsync(PaymentOptionCreateDto dto);
    Task<PaymentOptionGetDto> UpdateAsync(PaymentOptionUpdateDto dto);
    Task DeleteAsync(long id);
}
