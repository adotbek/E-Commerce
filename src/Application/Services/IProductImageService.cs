using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageDto>> GetAllAsync();
    Task<ProductImageDto?> GetByIdAsync(long id);
    Task<long> AddProductImageAsync(ProductImageDto dto);
    Task UpdateAsync(ProductImageDto dto, long id);
    Task DeleteAsync(long id);
}
