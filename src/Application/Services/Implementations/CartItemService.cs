using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _repository;

    public CartItemService(ICartItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddCartItemAsync(CartItemCreateDto dto)
    {
        var entity = CartItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<CartItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : CartItemMapper.ToDto(entity);
    }

    public async Task<IEnumerable<CartItemGetDto>> GetByCartIdAsync(long cartId)
    {
        var items = await _repository.GetByCartIdAsync(cartId);
        return items.Select(CartItemMapper.ToDto).ToList();
    }

    public async Task UpdateAsync(long id, CartItemUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Cart item with ID {id} not found.");

        CartItemMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
