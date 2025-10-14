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
        return entities.Select(FlashSaleItemMapper.ToGetDto);
    }

    public async Task<FlashSaleItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : FlashSaleItemMapper.ToGetDto(entity);
    }

    public async Task CreateService(FlashSaleItemGetDto dto)
    {
        var entity = FlashSaleItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(FlashSaleItemGetDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing is null)
            throw new KeyNotFoundException($"FlashSaleItem with Id={dto.Id} not found");

        FlashSaleItemMapper.UpdateEntity(existing, dto);
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
