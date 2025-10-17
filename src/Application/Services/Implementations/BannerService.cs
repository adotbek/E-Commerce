using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;

namespace Application.Services;

public class BannerService : IBannerService
{
    private readonly IBannerRepository _repository;

    public BannerService(IBannerRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddBannerAsync(BannerCreateDto dto)
    {
        var entity = BannerMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<ICollection<BannerGetDto>> GetAllAsync()
    {
        var banners = await _repository.GetAllAsync();
        return banners.Select(BannerMapper.ToDto).ToList();
    }

    public async Task<BannerGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : BannerMapper.ToDto(entity);
    }

    public async Task UpdateAsync(long id, BannerUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Banner with ID {id} not found.");

        BannerMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ICollection<BannerGetDto>> GetActiveAsync()
    {
        var banners = await _repository.GetAllAsync();
        var activeBanners = banners.Where(b => b.IsActive).ToList();
        return activeBanners.Select(BannerMapper.ToDto).ToList();
    }

    public async Task ToggleActiveAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Banner with ID {id} not found.");

        entity.IsActive = !entity.IsActive;
        await _repository.UpdateAsync(entity);
    }
}
