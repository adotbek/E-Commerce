using Application.Dtos;

namespace Application.Interfaces.Services;

public interface ICouponService
{
    Task<CouponGetDto> CreateAsync(CouponCreateDto dto);
    Task<CouponGetDto?> GetByIdAsync(long id);
    Task<CouponGetDto?> GetByCodeAsync(string code);
    Task<IEnumerable<CouponGetDto>> GetAllAsync();
    Task<CouponGetDto?> UpdateAsync(long id, CouponUpdateDto dto);
    Task<bool> DeleteAsync(long id);
}
