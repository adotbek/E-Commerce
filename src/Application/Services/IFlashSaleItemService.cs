namespace Application.Interfaces.Services;

using Application.Dtos;
using Application.DTOs.FlashSaleItems;

public interface IFlashSaleItemService
{
    Task<IEnumerable<FlashSaleItemGetDto>> GetAllAsync();
    Task<FlashSaleItemGetDto?> GetByIdAsync(long id);
    Task CreateService(FlashSaleItemGetDto dto);
    Task UpdateAsync(FlashSaleItemGetDto dto);
    Task DeleteAsync(long id);
}
