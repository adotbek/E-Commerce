using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageDto>> GetAllAsync();
    Task<ProductImageDto?> GetByIdAsync(long id);
    Task<long> AddProductImageAsync(ProductImageDto dto);
    Task UpdateAsync(ProductImageDto dto, long id);
    Task DeleteAsync(long id);
    Task<IEnumerable<ProductImageDto>> GetByProductIdAsync(long productId);
    Task<ProductImageDto?> GetMainImageByProductIdAsync(long productId);
    Task SetMainImageAsync(long imageId, long productId);
    Task SoftDeleteAsync(long id);
}
