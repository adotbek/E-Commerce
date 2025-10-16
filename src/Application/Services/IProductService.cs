using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(long id);
    Task<long> AddProductAsync(ProductDto dto, long categoryId);
    Task UpdateAsync(ProductDto dto, long categoryId, long id);
    Task DeleteAsync(long id);
}
