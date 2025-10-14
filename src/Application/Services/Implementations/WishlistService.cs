using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
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

    public async Task<WishlistGetDto> CreateAsync(WishlistCreateDto dto)
    {
        var entity = WishlistMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return WishlistMapper.ToGetDto(entity);
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
