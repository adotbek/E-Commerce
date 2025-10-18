using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .Include(p => p.Order)
            .Include(p => p.PaymentOption)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(long id)
    {
        return await _context.Payments
            .Include(p => p.Order)
            .Include(p => p.PaymentOption)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> AddAsync(Payment entity)
    {
        await _context.Payments.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Payment entity)
    {
        _context.Payments.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.Payments.FindAsync(id);
        if (existing is null)
            return;

        _context.Payments.Remove(existing);
        await _context.SaveChangesAsync();
    }

    // 🔹 Qo‘shimcha real funksiyalar

    public async Task<IEnumerable<Payment>> GetByUserIdAsync(long userId)
    {
        return await _context.Payments
            .Include(p => p.PaymentOption)
            .Include(p => p.Order)
            .Where(p => p.Order.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Payment?> GetByOrderIdAsync(long orderId)
    {
        return await _context.Payments
            .Include(p => p.PaymentOption)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
    {
        return await _context.Payments
            .Where(p => p.Status.ToLower() == status.ToLower())
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Payments
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPaidByUserAsync(long userId)
    {
        return await _context.Payments
            .Where(p => p.Order.UserId == userId && p.Status == "Completed")
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetTotalPaidInPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Payments
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.Status == "Completed")
            .SumAsync(p => p.Amount);
    }

    public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
    {
        return await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
    }

    public async Task UpdateStatusAsync(long paymentId, string newStatus)
    {
        var payment = await _context.Payments.FindAsync(paymentId);
        if (payment is null)
            throw new KeyNotFoundException($"Payment with ID {paymentId} not found.");

        payment.Status = newStatus;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsPaymentCompletedAsync(long orderId)
    {
        return await _context.Payments
            .AnyAsync(p => p.OrderId == orderId && p.Status == "Completed");
    }
}
