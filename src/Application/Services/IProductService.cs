using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(ProductDto dto, long categoryId, CancellationToken cancellationToken = default);
    Task<ProductDto> UpdateAsync(ProductDto dto, long categoryId, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
