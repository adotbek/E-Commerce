using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentOptionRepository
{
    Task<IEnumerable<PaymentOption>> GetAllAsync();
    Task<PaymentOption?> GetByIdAsync(long id);
    Task<PaymentOption> AddAsync(PaymentOption entity);
    Task<PaymentOption> UpdateAsync(PaymentOption entity);
    Task<bool> DeleteAsync(long id);
}
