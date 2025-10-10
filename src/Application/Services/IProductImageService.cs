using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductImageDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<ProductImageDto> CreateAsync(ProductImageDto dto, CancellationToken cancellationToken = default);
    Task<ProductImageDto> UpdateAsync(ProductImageDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
