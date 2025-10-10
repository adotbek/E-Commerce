using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class PaymentService(IPaymentRepository repository) : IPaymentService
{
    private readonly IPaymentRepository _repository = repository;

    public async Task<IEnumerable<PaymentGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Select(PaymentMapper.ToGetDto);

    public async Task<PaymentGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => (await _repository.GetByIdAsync(id, cancellationToken))?.ToGetDto();

    public async Task<PaymentGetDto> AddAsync(PaymentCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var added = await _repository.AddAsync(entity, cancellationToken);
        return added.ToGetDto();
    }

    public async Task<PaymentGetDto> UpdateAsync(PaymentUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken)
                     ?? throw new KeyNotFoundException("Payment not found");

        entity.UpdateEntity(dto);
        var updated = await _repository.UpdateAsync(entity, cancellationToken);
        return updated.ToGetDto();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        => await _repository.DeleteAsync(id, cancellationToken);
}
