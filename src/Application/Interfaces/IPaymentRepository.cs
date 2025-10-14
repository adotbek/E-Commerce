using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(long id);
    Task<Payment> AddAsync(Payment entity);
    Task<Payment> UpdateAsync(Payment entity);
    Task<bool> DeleteAsync(long id);
}
