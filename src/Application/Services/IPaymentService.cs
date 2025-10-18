using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IPaymentService
{
    Task<IEnumerable<PaymentGetDto>> GetAllAsync();
    Task<PaymentGetDto?> GetByIdAsync(long id);
    Task<long> AddPaymentAsync(PaymentCreateDto dto);
    Task UpdateAsync(PaymentUpdateDto dto, long id);
    Task<IEnumerable<PaymentGetDto>> GetByUserIdAsync(long userId);
    Task<Payment?> GetByOrderIdAsync(long orderId);
    Task<IEnumerable<PaymentGetDto>> GetByStatusAsync(string status);
    Task<IEnumerable<PaymentGetDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalPaidByUserAsync(long userId);
    Task<decimal> GetTotalPaidInPeriodAsync(DateTime startDate, DateTime endDate);
    Task<PaymentGetDto?> GetByTransactionIdAsync(string transactionId);
    Task UpdateStatusAsync(long paymentId, string newStatus);
    Task<bool> IsPaymentCompletedAsync(long orderId);
    Task DeleteAsync(long id);
}
