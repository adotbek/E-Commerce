using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaymentGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(PaymentMapper.ToGetDto);
    }

    public async Task<PaymentGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : PaymentMapper.ToGetDto(entity);
    }

    public async Task<PaymentGetDto> CreateAsync(PaymentCreateDto dto)
    {
        var entity = PaymentMapper.ToEntity(dto);
        var added = await _repository.AddAsync(entity);
        return PaymentMapper.ToGetDto(added);
    }

    public async Task<PaymentGetDto> UpdateAsync(PaymentUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);
        if (entity is null)
            throw new KeyNotFoundException($"Payment with Id={dto.Id} not found.");

        PaymentMapper.UpdateEntity(entity, dto);

        var updated = await _repository.UpdateAsync(entity);
        return PaymentMapper.ToGetDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException($"Payment with Id={id} not found.");
        return true;
    }
}
