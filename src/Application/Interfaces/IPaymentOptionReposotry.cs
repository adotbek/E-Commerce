using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentOptionRepository
{
    Task<IEnumerable<PaymentOption>> GetAllAsync();
    Task<PaymentOption?> GetByIdAsync(long id);
    Task<long> AddAsync(PaymentOption entity);
    Task UpdateAsync(PaymentOption entity);
    Task DeleteAsync(long id);
}
