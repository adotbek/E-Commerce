using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> AddCartAsync(CartCreateDto dto)
    {
        var entity = CartMapper.ToEntity(dto);

        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task<CartGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : CartMapper.ToDto(entity);
    }

    public async Task<CartGetDto?> GetByUserIdAsync(long userId)
    {
        var entity = await _repository.GetByUserIdAsync(userId);
        return entity is null ? null : CartMapper.ToDto(entity);
    }

    public async Task UpdateAsync(long userId, CartUpdateDto dto)
    {
        var entity = await _repository.GetByUserIdAsync(userId);
        if (entity is null)
            throw new KeyNotFoundException($"Cart for user ID {userId} not found.");

        CartMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await _repository.ExistsByUserIdAsync(userId);
    }

    public async Task<decimal> CalculateTotalPriceAsync(long cartId)
    {
        return await _repository.CalculateTotalPriceAsync(cartId);
    }

    public async Task ClearCartAsync(long cartId)
    {
        await _repository.ClearCartAsync(cartId);
    }
}
