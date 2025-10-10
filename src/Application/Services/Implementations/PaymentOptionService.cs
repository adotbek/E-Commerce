using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;

namespace Application.Services;

public class PaymentOptionService(IPaymentOptionRepository repository) : IPaymentOptionService
{
    private readonly IPaymentOptionRepository _repository = repository;

    public async Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(PaymentOptionMapper.ToGetDto);
    }

    public async Task<PaymentOptionGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : PaymentOptionMapper.ToGetDto(entity);
    }

    public async Task<PaymentOptionGetDto> AddAsync(PaymentOptionCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = PaymentOptionMapper.ToEntity(dto);
        var added = await _repository.AddAsync(entity, cancellationToken);
        return PaymentOptionMapper.ToGetDto(added);
    }

    public async Task<PaymentOptionGetDto> UpdateAsync(PaymentOptionUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"PaymentOption with Id={dto.Id} not found");

        PaymentOptionMapper.UpdateEntity(entity, dto);
        var updated = await _repository.UpdateAsync(entity, cancellationToken);
        return PaymentOptionMapper.ToGetDto(updated);
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}
