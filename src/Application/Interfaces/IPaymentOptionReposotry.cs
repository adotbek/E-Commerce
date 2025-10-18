using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentOptionRepository
{
    Task<IEnumerable<PaymentOption>> GetAllAsync();
    Task<PaymentOption?> GetByIdAsync(long id);
    Task<long> AddAsync(PaymentOption entity);
    Task UpdateAsync(PaymentOption entity);
    Task DeleteAsync(long id);

    Task<IEnumerable<PaymentOption>> GetByUserIdAsync(long userId);
    Task<IEnumerable<PaymentOption>> GetActiveByUserIdAsync(long userId);
    Task<PaymentOption?> GetDefaultByUserIdAsync(long userId);

    Task SetDefaultAsync(long userId, long paymentOptionId);
    Task<bool> BelongsToUserAsync(long paymentOptionId, long userId);
    Task<bool> ExistsByCardNumberAsync(string cardNumber, long userId);
    Task<bool> IsExpiredAsync(long id);
    Task<string?> GetMaskedCardNumberAsync(long id);
    Task ToggleActiveAsync(long id, bool isActive);
    Task<string> GeneratePaymentTokenAsync(long id);
}
