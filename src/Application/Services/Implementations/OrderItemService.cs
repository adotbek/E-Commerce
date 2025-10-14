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
        return entities.Select(OrderItemMapper.ToGetDto);
    }

    public async Task<OrderItemGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : OrderItemMapper.ToGetDto(entity);
    }

    public async Task<OrderItemGetDto> CreateAsync(OrderItemCreateDto dto)
    {
        var entity = OrderItemMapper.ToEntity(dto);
        var created = await _repository.AddAsync(entity);
        return OrderItemMapper.ToGetDto(created);
    }

    public async Task<OrderItemGetDto> UpdateAsync(OrderItemUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"OrderItem with Id={dto.Id} not found.");

        OrderItemMapper.UpdateEntity(existing, dto);
        var updated = await _repository.UpdateAsync(existing);
        return OrderItemMapper.ToGetDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"OrderItem with Id={id} not found.");

        await _repository.DeleteAsync(id);
    }
}
