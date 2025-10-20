using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _repository;

    public OrderItemService(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrderItemGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(OrderItemMapper.ToDto);
    }

    public async Task<OrderItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : OrderItemMapper.ToDto(entity);
    }

    public async Task<long> AddOrderAsync(OrderItemCreateDto dto)
    {
        var entity = OrderItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(OrderItemUpdateDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Order item with ID {id} not found.");

        OrderItemMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<OrderItemGetDto>> GetByOrderIdAsync(long orderId)
    {
        var entities = await _repository.GetByOrderIdAsync(orderId);
        return entities.Select(OrderItemMapper.ToDto);
    }

    public async Task<decimal> CalculateTotalAsync(long orderId)
    {
        return await _repository.CalculateTotalAsync(orderId);
    }

    public async Task<bool> ExistsAsync(long orderItemId)
    {
        return await _repository.ExistsAsync(orderItemId);
    }

    public async Task<int> GetTotalQuantityAsync(long orderId)
    {
        return await _repository.GetTotalQuantityAsync(orderId);
    }

    public async Task AddOrUpdateItemAsync(long orderId, long productId, int quantity)
    {
        await _repository.AddOrUpdateItemAsync(orderId, productId, quantity);
    }

    public async Task DeleteByOrderIdAsync(long orderId)
    {
        await _repository.DeleteByOrderIdAsync(orderId);
    }

    public async Task<bool> ExistsInOrderAsync(long orderId, long productId)
    {
        return await _repository.ExistsInOrderAsync(orderId, productId);
    }
}
