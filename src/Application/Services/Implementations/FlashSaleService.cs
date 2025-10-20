using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class FlashSaleService : IFlashSaleService
{
    private readonly IFlashSaleRepository _repository;

    public FlashSaleService(IFlashSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddFlashSaleAsync(FlashSaleCreateDto dto)
    {
        var entity = FlashSaleMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<FlashSaleGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : FlashSaleMapper.ToDto(entity);
    }

    public async Task<IEnumerable<FlashSaleGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(FlashSaleMapper.ToDto);
    }

    public async Task<IEnumerable<FlashSaleGetDto>> GetActiveAsync(DateTime? at = null)
    {
        var date = at ?? DateTime.UtcNow;
        var activeSales = await _repository.GetActiveAsync(date);
        return activeSales.Select(FlashSaleMapper.ToDto);
    }

    public async Task UpdateAsync(long id, FlashSaleUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"FlashSale with ID {id} not found.");

        FlashSaleMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<int> RemoveExpiredAsync(DateTime? now = null)
    {
        return await _repository.RemoveExpiredAsync(now);
    }

    public async Task<FlashSaleGetDto?> GetActiveByProductIdAsync(long productId)
    {
        var entity = await _repository.GetActiveByProductIdAsync(productId);
        return entity is null ? null : FlashSaleMapper.ToDto(entity);
    }

    public async Task<bool> IsActiveAsync(long flashSaleId, DateTime? now = null)
    {
        return await _repository.IsActiveAsync(flashSaleId, now);
    }
}
