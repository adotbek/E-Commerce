using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class BannerService(IBannerRepository repository) : IBannerService
{
    public async Task<BannerGetDto> CreateAsync(BannerCreateDto dto)
    {
        var entity = BannerMapper.ToEntity(dto);
        var created = await repository.CreateAsync(entity);
        return BannerMapper.ToGetDto(created);
    }

    public async Task<ICollection<BannerGetDto>> GetAllAsync()
    {
        var banners = await repository.GetAllAsync();
        return banners.Select(BannerMapper.ToGetDto).ToList();
    }

    public async Task<BannerGetDto?> GetByIdAsync(long id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity is null ? null : BannerMapper.ToGetDto(entity);
    }

    public async Task<BannerGetDto?> UpdateAsync(long id, BannerUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) return null;

        BannerMapper.UpdateEntity(entity, dto);
        var updated = await repository.UpdateAsync(entity);
        return BannerMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await repository.DeleteAsync(id);
    }
}
