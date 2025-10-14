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
        return entities.Select(OrderMapper.ToGetDto);
    }

    public async Task<OrderGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : OrderMapper.ToGetDto(entity);
    }

    public async Task<OrderGetDto> CreateAsync(OrderCreateDto dto)
    {
        var entity = OrderMapper.ToEntity(dto);
        var created = await _repository.AddAsync(entity);
        return OrderMapper.ToGetDto(created);
    }

    public async Task<OrderGetDto> UpdateAsync(OrderUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Order with Id={dto.Id} not found.");

        OrderMapper.UpdateEntity(existing, dto);
        var updated = await _repository.UpdateAsync(existing);
        return OrderMapper.ToGetDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Order with Id={id} not found.");

        await _repository.DeleteAsync(id);
    }
}
