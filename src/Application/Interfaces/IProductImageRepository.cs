using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage?> GetByIdAsync(long id);
    Task<ProductImage> AddAsync(ProductImage entity);
    Task<ProductImage> UpdateAsync(ProductImage entity);
    Task<bool> DeleteAsync(long id);
}
