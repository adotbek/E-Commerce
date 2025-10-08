using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CouponService(ICouponRepository repository) : ICouponService
{
    public async Task<CouponGetDto> CreateAsync(CouponCreateDto dto)
    {
        var entity = CouponMapper.ToEntity(dto);
        var created = await repository.CreateAsync(entity);
        return CouponMapper.ToGetDto(created);
    }

    public async Task<CouponGetDto?> GetByIdAsync(long id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity is null ? null : CouponMapper.ToGetDto(entity);
    }

    public async Task<CouponGetDto?> GetByCodeAsync(string code)
    {
        var entity = await repository.GetByCodeAsync(code);
        return entity is null ? null : CouponMapper.ToGetDto(entity);
    }

    public async Task<IEnumerable<CouponGetDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(CouponMapper.ToGetDto);
    }

    public async Task<CouponGetDto?> UpdateAsync(long id, CouponUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) return null;

        CouponMapper.UpdateEntity(entity, dto);
        var updated = await repository.UpdateAsync(entity);
        return CouponMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await repository.DeleteAsync(id);
    }
}
