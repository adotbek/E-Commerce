using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CartItemService(ICartItemRepository repository) : ICartItemService
{
    public async Task<CartItemGetDto> CreateAsync(CartItemCreateDto dto)
    {
        var entity = CartItemMapper.ToEntity(dto);
        var created = await repository.CreateAsync(entity);
        return CartItemMapper.ToGetDto(created);
    }

    public async Task<CartItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity is null ? null : CartItemMapper.ToGetDto(entity);
    }

    public async Task<IEnumerable<CartItemGetDto>> GetByCartIdAsync(long cartId)
    {
        var items = await repository.GetByCartIdAsync(cartId);
        return items.Select(CartItemMapper.ToGetDto);
    }

    public async Task<CartItemGetDto?> UpdateAsync(long id, CartItemUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) return null;

        CartItemMapper.UpdateEntity(entity, dto);
        var updated = await repository.UpdateAsync(entity);
        return CartItemMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await repository.DeleteAsync(id);
    }
}
