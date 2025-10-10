using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default);
    Task<Payment> UpdateAsync(Payment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
