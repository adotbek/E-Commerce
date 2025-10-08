using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<Coupon> CreateAsync(Coupon entity);
    Task<Coupon?> GetByIdAsync(long id);
    Task<Coupon?> GetByCodeAsync(string code);
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<Coupon> UpdateAsync(Coupon entity);
    Task<bool> DeleteAsync(long id);
}
