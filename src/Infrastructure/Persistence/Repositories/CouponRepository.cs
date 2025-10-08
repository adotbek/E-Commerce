using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CouponRepository(AppDbContext context) : ICouponRepository
{
    public async Task<Coupon> CreateAsync(Coupon entity)
    {
        await context.Coupons.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Coupon?> GetByIdAsync(long id)
    {
        return await context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        return await context.Coupons
            .FirstOrDefaultAsync(c => c.Code == code && c.IsActive && c.ValidUntil > DateTime.UtcNow);
    }

    public async Task<IEnumerable<Coupon>> GetAllAsync()
    {
        return await context.Coupons.ToListAsync();
    }

    public async Task<Coupon> UpdateAsync(Coupon entity)
    {
        context.Coupons.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
        if (entity is null) return false;

        context.Coupons.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
