using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(long id);
    Task<long> AddAsync(Payment entity);
    Task UpdateAsync(Payment entity);
    Task DeleteAsync(long id);
}
