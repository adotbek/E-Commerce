using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Repositories;

namespace Application.Services;

public class WishlistItemService : IWishlistItemService
{
    private readonly IWishlistItemRepository _repository;

    public WishlistItemService(IWishlistItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WishlistItemGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(WishlistItemMapper.ToGetDto).ToList();
    }

    public async Task<WishlistItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : WishlistItemMapper.ToGetDto(entity);
    }

    public async Task<WishlistItemGetDto> CreateAsync(WishlistItemGetDto dto)
    {
        var entity = WishlistItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return WishlistItemMapper.ToGetDto(entity);
    }

    public async Task<WishlistItemGetDto?> UpdateAsync(long id, WishlistItemGetDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            return null;

        existing.ProductId = dto.ProductId;
        await _repository.UpdateAsync(existing);        
        return WishlistItemMapper.ToGetDto(existing);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            return false;

        await _repository.DeleteAsync(entity);
        return true;
    }
}
