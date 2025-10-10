namespace Application.Services;

using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

public class FlashSaleItemService(IFlashSaleItemRepository repository) : IFlashSaleItemService
{
    private readonly IFlashSaleItemRepository _repository = repository;

    public async Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(FlashSaleItemMapper.ToGetDto).ToList();
    }

    public async Task<FlashSaleItemGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return FlashSaleItemMapper.ToGetDto(entity);
    }

    public async Task AddAsync(FlashSaleItemGetDto dto, CancellationToken cancellationToken = default)
    {
        var entity = FlashSaleItemMapper.ToEntity(dto);
        await _repository.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(FlashSaleItemGetDto dto, CancellationToken cancellationToken = default)
    {
        var entity = FlashSaleItemMapper.ToEntity(dto);
        await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
