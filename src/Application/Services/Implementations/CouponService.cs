using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _repository;

    public CouponService(ICouponRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddCouponAsync(CouponCreateDto dto)
    {
        var entity = CouponMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<CouponGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : CouponMapper.ToDto(entity);
    }

    public async Task<CouponGetDto?> GetByCodeAsync(string code)
    {
        var entity = await _repository.GetByCodeAsync(code);
        return entity is null ? null : CouponMapper.ToDto(entity);
    }

    public async Task<IEnumerable<CouponGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(CouponMapper.ToDto);
    }

    public async Task UpdateAsync(long id, CouponUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Coupon with ID {id} not found.");

        CouponMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ValidateCouponAsync(string code)
    {
        return await _repository.ValidateCouponAsync(code);
    }

    public async Task<decimal> ApplyCouponAsync(string code, decimal totalPrice)
    {
        return await _repository.ApplyCouponAsync(code, totalPrice);
    }

    public async Task<IEnumerable<CouponGetDto>> GetActiveCouponsAsync()
    {
        var coupons = await _repository.GetActiveCouponsAsync();
        return coupons.Select(CouponMapper.ToDto);
    }
}
