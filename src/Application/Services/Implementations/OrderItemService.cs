using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class OrderItemService(IOrderItemRepository repository) : IOrderItemService
{
    private readonly IOrderItemRepository _repository = repository;

    public async Task<IEnumerable<OrderItemGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(OrderItemMapper.ToGetDto);
    }

    public async Task<OrderItemGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : OrderItemMapper.ToGetDto(entity);
    }

    public async Task<OrderItemGetDto> AddAsync(OrderItemCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = OrderItemMapper.ToEntity(dto);
        var created = await _repository.AddAsync(entity, cancellationToken);
        return OrderItemMapper.ToGetDto(created);
    }

    public async Task<OrderItemGetDto> UpdateAsync(OrderItemUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException("OrderItem not found");

        OrderItemMapper.UpdateEntity(entity, dto);
        var updated = await _repository.UpdateAsync(entity, cancellationToken);
        return OrderItemMapper.ToGetDto(updated);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
