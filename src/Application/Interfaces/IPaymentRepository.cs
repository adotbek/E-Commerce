using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(long id);
    Task<long> AddAsync(Payment entity);
    Task UpdateAsync(Payment entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<Payment>> GetByUserIdAsync(long userId);
    Task<Payment?> GetByOrderIdAsync(long orderId);
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
    Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalPaidByUserAsync(long userId);
    Task<decimal> GetTotalPaidInPeriodAsync(DateTime startDate, DateTime endDate);
    Task<Payment?> GetByTransactionIdAsync(string transactionId);
    Task UpdateStatusAsync(long paymentId, PaymentStatus newStatus);
    Task<bool> IsPaymentCompletedAsync(long orderId);
}
