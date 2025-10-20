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
        var exists = await _repository.ExistsAsync(dto.ProductId, dto.FlashSaleId);
        if (exists)
            throw new InvalidOperationException("This product is already added to the flash sale.");

        var entity = FlashSaleItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task UpdateAsync(FlashSaleItemUpdateDto dto, long id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"FlashSaleItem with Id={id} not found.");

        existing.ProductId = dto.ProductId;
        existing.FlashSaleId = dto.FlashSaleId;
        existing.FlashSale.DiscountedPrice = dto.DiscountedPrice;

        await _repository.UpdateAsync(existing);
    }

    public async Task<IEnumerable<FlashSaleItemGetDto>> GetByFlashSaleIdAsync(long flashSaleId)
    {
        var items = await _repository.GetByFlashSaleIdAsync(flashSaleId);
        return items.Select(FlashSaleItemMapper.ToDto);
    }

    public async Task<FlashSaleItemGetDto?> GetByProductIdAsync(long productId)
    {
        var item = await _repository.GetByProductIdAsync(productId);
        return item is null ? null : FlashSaleItemMapper.ToDto(item);
    }

    public async Task<IEnumerable<FlashSaleItemGetDto>> GetActiveItemsAsync(DateTime now)
    {
        var items = await _repository.GetActiveItemsAsync(now);
        return items.Select(FlashSaleItemMapper.ToDto);
    }

    public async Task<decimal> CalculateDiscountedPriceAsync(long productId, long flashSaleId)
    {
        return await _repository.CalculateDiscountedPriceAsync(productId, flashSaleId);
    }

    public async Task<bool> ExistsAsync(long productId, long flashSaleId)
    {
        return await _repository.ExistsAsync(productId, flashSaleId);
    }

    public async Task<int> RemoveExpiredItemsAsync(DateTime now)
    {
        return await _repository.RemoveExpiredItemsAsync(now);
    }
}
