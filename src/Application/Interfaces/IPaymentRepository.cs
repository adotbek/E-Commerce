using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<ICollection<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(long id);
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
    Task DeleteAsync(long id);
}
