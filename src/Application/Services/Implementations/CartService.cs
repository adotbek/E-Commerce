using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CartService(ICartRepository repository) : ICartService
{
    public async Task<CartGetDto> CreateAsync(CartCreateDto dto)
    {
        var entity = CartMapper.ToEntity(dto);
        var created = await repository.CreateAsync(entity);
        return CartMapper.ToGetDto(created);
    }

    public async Task<CartGetDto?> GetByIdAsync(long id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity is null ? null : CartMapper.ToGetDto(entity);
    }

    public async Task<CartGetDto?> GetByUserIdAsync(long userId)
    {
        var entity = await repository.GetByUserIdAsync(userId);
        return entity is null ? null : CartMapper.ToGetDto(entity);
    }

    public async Task<CartGetDto?> UpdateAsync(long id, CartUpdateDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null) return null;

        CartMapper.UpdateEntity(entity, dto);
        var updated = await repository.UpdateAsync(entity);
        return CartMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await repository.DeleteAsync(id);
    }
}
