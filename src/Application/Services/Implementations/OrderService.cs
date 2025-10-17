using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrderGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(OrderMapper.ToDto);
    }

    public async Task<OrderGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : OrderMapper.ToDto(entity);
    }

    public async Task<long> AddOrderAsync(OrderCreateDto dto)
    {
        var entity = OrderMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(OrderUpdateDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Order with ID {id} not found.");

        OrderMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<OrderGetDto>> GetByUserIdAsync(long userId)
    {
        var orders = await _repository.GetByUserIdAsync(userId);
        return orders.Select(OrderMapper.ToDto);
    }

    public async Task UpdateStatusAsync(long id, string status)
    {
        await _repository.UpdateStatusAsync(id, status);
    }

    public async Task<IEnumerable<OrderGetDto>> GetByStatusAsync(string status)
    {
        var orders = await _repository.GetByStatusAsync(status);
        return orders.Select(OrderMapper.ToDto);
    }
}
