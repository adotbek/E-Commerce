using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Core.Errors;
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
        return entities.Select(WishlistItemMapper.ToDto).ToList();
    }

    public async Task<WishlistItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : WishlistItemMapper.ToDto(entity);
    }

    public async Task<long> AddWishlistItemAsync(WishlistItemGetDto dto)
    {
        var entity = WishlistItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(long id, WishlistItemGetDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new EntityNotFoundException($"Wishlist item with ID {id} not found");

        WishlistItemMapper.UpdateEntity(existing, dto);
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new EntityNotFoundException($"Wishlist item with ID {id} not found");

        await _repository.DeleteAsync(existing);
    }

    public async Task<IEnumerable<WishlistItemGetDto>> GetByUserIdAsync(long userId)
    {
        var items = await _repository.GetByUserIdAsync(userId);
        return items.Select(WishlistItemMapper.ToDto).ToList();
    }

    public async Task<IEnumerable<WishlistItemGetDto>> GetByWishlistIdAsync(long wishlistId)
    {
        var items = await _repository.GetByWishlistIdAsync(wishlistId);
        return items.Select(WishlistItemMapper.ToDto).ToList();
    }

    public async Task<bool> ExistsAsync(long wishlistId, long productId)
    {
        return await _repository.ExistsAsync(wishlistId, productId);
    }

    public async Task<int> GetCountByWishlistIdAsync(long wishlistId)
    {
        return await _repository.GetCountByWishlistIdAsync(wishlistId);
    }

    public async Task ClearWishlistAsync(long wishlistId)
    {
        await _repository.ClearWishlistAsync(wishlistId);
    }
}
