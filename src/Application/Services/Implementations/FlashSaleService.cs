using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class FlashSaleService : IFlashSaleService
{
    private readonly IFlashSaleRepository _repo;

    public FlashSaleService(IFlashSaleRepository repo)
    {
        _repo = repo;
    }

    public async Task<FlashSaleGetDto> CreateAsync(FlashSaleCreateDto dto)
    {
        var entity = FlashSaleMapper.ToEntity(dto);
        var created = await _repo.CreateAsync(entity);
        return FlashSaleMapper.ToGetDto(created);
    }

    public async Task<FlashSaleGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? null : FlashSaleMapper.ToGetDto(entity);
    }

    public async Task<IEnumerable<FlashSaleGetDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(FlashSaleMapper.ToGetDto).ToList();
    }

    public async Task<IEnumerable<FlashSaleGetDto>> GetActiveAsync(DateTime? at = null)
    {
        var when = at ?? DateTime.UtcNow;
        var list = await _repo.GetActiveAsync(when);
        return list.Select(FlashSaleMapper.ToGetDto).ToList();
    }

    public async Task<FlashSaleGetDto?> UpdateAsync(long id, FlashSaleUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return null;

        FlashSaleMapper.UpdateEntity(existing, dto);
        var updated = await _repo.UpdateAsync(existing);
        return FlashSaleMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.DeleteAsync(id);
    }
}
