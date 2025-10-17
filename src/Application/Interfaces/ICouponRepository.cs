using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<long> AddAsync(Coupon entity);
    Task<Coupon?> GetByIdAsync(long id);
    Task<Coupon?> GetByCodeAsync(string code);
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task UpdateAsync(Coupon entity);
    Task DeleteAsync(long id);
    Task<bool> ValidateCouponAsync(string code);
    Task<decimal> ApplyCouponAsync(string code, decimal totalPrice);
    Task<IEnumerable<Coupon>> GetActiveCouponsAsync();

}
