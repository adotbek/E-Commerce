using Application.Dtos;

namespace Application.Interfaces.Services;

public interface IPaymentOptionService
{
    Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync();
    Task<PaymentOptionGetDto?> GetByIdAsync(long id);
    Task<long> AddPaymentOptionAsync(PaymentOptionCreateDto dto);
    Task UpdateAsync(PaymentOptionUpdateDto dto, long id);
    Task DeleteAsync(long id);  
    Task<IEnumerable<PaymentOptionGetDto>> GetByUserIdAsync(long userId);
    Task<IEnumerable<PaymentOptionGetDto>> GetActiveByUserIdAsync(long userId);
    Task<PaymentOptionGetDto?> GetDefaultByUserIdAsync(long userId);
    Task SetDefaultAsync(long userId, long paymentOptionId);
    Task<bool> BelongsToUserAsync(long paymentOptionId, long userId);
    Task<bool> ExistsByCardNumberAsync(string cardNumber, long userId);
    Task<bool> IsExpiredAsync(long id);
    Task<string?> GetMaskedCardNumberAsync(long id);
    Task ToggleActiveAsync(long id, bool isActive);
    Task<string> GeneratePaymentTokenAsync(long id);

}
