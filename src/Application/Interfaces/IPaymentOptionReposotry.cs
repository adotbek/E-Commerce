using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentOptionRepository
{
    Task<IEnumerable<PaymentOption>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentOption?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaymentOption> AddAsync(PaymentOption entity, CancellationToken cancellationToken = default);
    Task<PaymentOption> UpdateAsync(PaymentOption entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
