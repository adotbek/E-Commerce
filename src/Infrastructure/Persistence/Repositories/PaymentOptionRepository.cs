using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Repositories;

public class PaymentOptionRepository : IPaymentOptionRepository
{
    private readonly AppDbContext _context;

    public PaymentOptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaymentOption>> GetAllAsync()
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PaymentOption?> GetByIdAsync(long id)
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> AddAsync(PaymentOption entity)
    {
        await _context.PaymentOptions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(PaymentOption entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.PaymentOptions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.PaymentOptions.FindAsync(id);
        if (existing is null)
            return;

        _context.PaymentOptions.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PaymentOption>> GetByUserIdAsync(long userId)
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PaymentOption>> GetActiveByUserIdAsync(long userId)
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .Where(p => p.UserId == userId && p.IsActive)
            .ToListAsync();
    }

    public async Task<PaymentOption?> GetDefaultByUserIdAsync(long userId)
    {
        return await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && p.IsDefault);
    }

    public async Task SetDefaultAsync(long userId, long paymentOptionId)
    {
        var userCards = await _context.PaymentOptions
            .Where(p => p.UserId == userId)
            .ToListAsync();

        foreach (var card in userCards)
            card.IsDefault = card.Id == paymentOptionId;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> BelongsToUserAsync(long paymentOptionId, long userId)
    {
        return await _context.PaymentOptions
            .AnyAsync(p => p.Id == paymentOptionId && p.UserId == userId);
    }

    public async Task<bool> ExistsByCardNumberAsync(string cardNumber, long userId)
    {
        return await _context.PaymentOptions
            .AnyAsync(p => p.CardNumber == cardNumber && p.UserId == userId);
    }

    public async Task<bool> IsExpiredAsync(long id)
    {
        var entity = await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity is null)
            return true;

        if (entity.ExpiryMonth <= 0 || entity.ExpiryYear <= 0)
            return true;

        var expiry = new DateTime(entity.ExpiryYear, entity.ExpiryMonth, 1)
            .AddMonths(1)
            .AddDays(-1);

        return DateTime.UtcNow > expiry;
    }


    public async Task<string?> GetMaskedCardNumberAsync(long id)
    {
        var entity = await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity is null)
            return null;

        if (entity.CardNumber.Length < 4)
            return "****";

        return $"**** **** **** {entity.CardNumber[^4..]}";
    }

    public async Task ToggleActiveAsync(long id, bool isActive)
    {
        var entity = await _context.PaymentOptions.FindAsync(id);
        if (entity is null)
            return;

        entity.IsActive = isActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<string> GeneratePaymentTokenAsync(long id)
    {
        var entity = await _context.PaymentOptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (entity is null)
            throw new KeyNotFoundException("Payment option not found.");

        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("=", "")
            .Replace("+", "")
            .Replace("/", "")
            .Substring(0, 16);
    }
}
