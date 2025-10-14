using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageDto>> GetAllAsync();
    Task<ProductImageDto?> GetByIdAsync(long id);
    Task<ProductImageDto> CreateAsync(ProductImageDto dto);
    Task<ProductImageDto> UpdateAsync(ProductImageDto dto);
    Task DeleteAsync(long id);
}
