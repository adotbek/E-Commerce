using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;

    public CouponRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<long> AddAsync(Coupon entity)
    {
        await _context.Coupons.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Coupon?> GetByIdAsync(long id)
    {
        return await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        return await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<IEnumerable<Coupon>> GetAllAsync()
    {
        return await _context.Coupons
            .OrderByDescending(c => c.ValidUntil)
            .ToListAsync();
    }

    public async Task UpdateAsync(Coupon entity)
    {
        _context.Coupons.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.Coupons.FindAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Coupon with Id={id} not found.");

        _context.Coupons.Remove(existing);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ValidateCouponAsync(string code)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
        if (coupon is null) return false;
        if (!coupon.IsActive) return false;
        if (coupon.ValidUntil < DateTime.UtcNow) return false;

        return true;
    }

    public async Task<decimal> ApplyCouponAsync(string code, decimal totalPrice)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
        if (coupon is null || !coupon.IsActive || coupon.ValidUntil < DateTime.UtcNow)
            throw new InvalidOperationException("Kupon yaroqsiz yoki muddati o‘tgan.");

        var discountAmount = totalPrice * (decimal)(coupon.DiscountPercent / 100.0);
        var newTotal = totalPrice - discountAmount;

        return newTotal < 0 ? 0 : newTotal;
    }

    public async Task<IEnumerable<Coupon>> GetActiveCouponsAsync()
    {
        return await _context.Coupons
            .Where(c => c.IsActive && c.ValidUntil >= DateTime.UtcNow)
            .OrderBy(c => c.ValidUntil)
            .ToListAsync();
    }
}
