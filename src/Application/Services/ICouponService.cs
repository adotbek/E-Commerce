using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICouponService
{
    Task<long> AddCouponAsync(CouponCreateDto dto);
    Task<CouponGetDto?> GetByIdAsync(long id);
    Task<CouponGetDto?> GetByCodeAsync(string code);
    Task<IEnumerable<CouponGetDto>> GetAllAsync();
    Task UpdateAsync(long id, CouponUpdateDto dto);
    Task DeleteAsync(long id);
}
