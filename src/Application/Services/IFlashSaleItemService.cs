namespace Application.Interfaces.Services;

using Application.Dtos;
using Application.DTOs.FlashSaleItems;

public interface IFlashSaleItemService
{
    Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync();
    Task<FlashSaleItemGetDto?> GetByIdAsync(long id);
    Task<long> AddFlashSaleItemService(FlashSaleItemCreateDto dto);
    Task UpdateAsync(FlashSaleItemGetDto dto, long id);
    Task DeleteAsync(long id);
}
