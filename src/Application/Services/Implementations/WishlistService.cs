using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Core.Errors;
using Domain.Repositories;

namespace Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _repository;

    public WishlistService(IWishlistRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WishlistGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(WishlistMapper.ToGetDto).ToList();
    }

    public async Task<WishlistGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : WishlistMapper.ToGetDto(entity);
    }

    public async Task<long> AddWishlistAsync(WishlistCreateDto dto)
    {
        var entity = WishlistMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(WishlistCreateDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException($"Wishlist with ID {id} not found");

        WishlistMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new EntityNotFoundException($"Wishlist with ID {id} not found");

        await _repository.DeleteAsync(entity);
    }
}
