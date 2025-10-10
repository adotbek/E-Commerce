using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class OrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository _repository = repository;

    public async Task<IEnumerable<OrderGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(OrderMapper.ToGetDto);
    }

    public async Task<OrderGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : OrderMapper.ToGetDto(entity);
    }

    public async Task<OrderGetDto> CreateAsync(OrderCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = OrderMapper.ToEntity(dto);
        var created = await _repository.AddAsync(entity, cancellationToken);
        return OrderMapper.ToGetDto(created);
    }

    public async Task<OrderGetDto?> UpdateAsync(OrderUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (existing is null) return null;

        OrderMapper.UpdateEntity(existing, dto);
        var updated = await _repository.UpdateAsync(existing, cancellationToken);
        return OrderMapper.ToGetDto(updated);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
