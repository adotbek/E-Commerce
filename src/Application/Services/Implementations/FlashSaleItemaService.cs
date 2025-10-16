using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.DTOs.FlashSaleItems;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class FlashSaleItemService : IFlashSaleItemService
{
    private readonly IFlashSaleItemRepository _repository;

    public FlashSaleItemService(IFlashSaleItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(FlashSaleItemMapper.ToDto);
    }

    public async Task<FlashSaleItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : FlashSaleItemMapper.ToDto(entity);
    }

    public async Task<long> AddFlashSaleItemService(FlashSaleItemCreateDto dto)
    {
        var entity = FlashSaleItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(FlashSaleItemGetDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"FlashSaleItem with ID {id} not found.");

        FlashSaleItemMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
