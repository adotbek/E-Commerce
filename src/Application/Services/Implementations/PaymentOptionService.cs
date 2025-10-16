using Application.Common.Interfaces.Repositories;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

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
        return entities.Select(PaymentOptionMapper.ToDto);
    }

    public async Task<PaymentOptionGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : PaymentOptionMapper.ToDto(entity);
    }

    public async Task<long> AddPaymentOptionAsync(PaymentOptionCreateDto dto)
    {
        var entity = PaymentOptionMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(PaymentOptionUpdateDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Payment option with ID {id} not found.");

        PaymentOptionMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
