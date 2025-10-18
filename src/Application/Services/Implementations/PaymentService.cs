using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Dtos;
using Application.Mappers;
using Domain.Entities;

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
        return entities.Select(PaymentMapper.ToDto);
    }

    public async Task<PaymentGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : PaymentMapper.ToDto(entity);
    }

    public async Task<long> AddPaymentAsync(PaymentCreateDto dto)
    {
        var entity = PaymentMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(PaymentUpdateDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Payment with ID {id} not found.");

        PaymentMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    // 🔹 Qo‘shimcha funksiyalar

    public async Task<IEnumerable<PaymentGetDto>> GetByUserIdAsync(long userId)
    {
        var entities = await _repository.GetByUserIdAsync(userId);
        return entities.Select(PaymentMapper.ToDto);
    }

    public async Task<Payment?> GetByOrderIdAsync(long orderId)
    {
        return await _repository.GetByOrderIdAsync(orderId);
    }

    public async Task<IEnumerable<PaymentGetDto>> GetByStatusAsync(string status)
    {
        var entities = await _repository.GetByStatusAsync(status);
        return entities.Select(PaymentMapper.ToDto);
    }

    public async Task<IEnumerable<PaymentGetDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var entities = await _repository.GetByDateRangeAsync(startDate, endDate);
        return entities.Select(PaymentMapper.ToDto);
    }

    public async Task<decimal> GetTotalPaidByUserAsync(long userId)
    {
        return await _repository.GetTotalPaidByUserAsync(userId);
    }

    public async Task<decimal> GetTotalPaidInPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _repository.GetTotalPaidInPeriodAsync(startDate, endDate);
    }

    public async Task<PaymentGetDto?> GetByTransactionIdAsync(string transactionId)
    {
        var entity = await _repository.GetByTransactionIdAsync(transactionId);
        return entity is null ? null : PaymentMapper.ToDto(entity);
    }

    public async Task UpdateStatusAsync(long paymentId, string newStatus)
    {
        await _repository.UpdateStatusAsync(paymentId, newStatus);
    }

    public async Task<bool> IsPaymentCompletedAsync(long orderId)
    {
        return await _repository.IsPaymentCompletedAsync(orderId);
    }
}
