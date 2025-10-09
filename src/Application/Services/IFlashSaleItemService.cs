namespace Application.Interfaces.Services;

using Application.Dtos;
using Application.DTOs.FlashSaleItems;

public interface IFlashSaleItemService
{
    Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlashSaleItemGetDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task AddAsync(FlashSaleItemGetDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(FlashSaleItemGetDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
