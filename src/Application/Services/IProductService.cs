using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(long id);
    Task<ProductDto> CreateAsync(ProductDto dto, long categoryId);
    Task<ProductDto> UpdateAsync(ProductDto dto, long categoryId);
    Task DeleteAsync(long id);
}
