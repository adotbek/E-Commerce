using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;

namespace Application.Services;

public class PaymentOptionService : IPaymentOptionService
{
    private readonly IPaymentOptionRepository _repository;

    public PaymentOptionService(IPaymentOptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaymentOptionGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(PaymentOptionMapper.ToGetDto);
    }

    public async Task<PaymentOptionGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : PaymentOptionMapper.ToGetDto(entity);
    }

    public async Task<PaymentOptionGetDto> CreateAsync(PaymentOptionCreateDto dto)
    {
        var entity = PaymentOptionMapper.ToEntity(dto);
        var added = await _repository.AddAsync(entity);
        return PaymentOptionMapper.ToGetDto(added);
    }

    public async Task<PaymentOptionGetDto> UpdateAsync(PaymentOptionUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);
        if (entity is null)
            throw new KeyNotFoundException($"PaymentOption with Id={dto.Id} not found.");

        PaymentOptionMapper.UpdateEntity(entity, dto);

        var updated = await _repository.UpdateAsync(entity);
        return PaymentOptionMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException($"PaymentOption with Id={id} not found.");
        return true;
    }
}
