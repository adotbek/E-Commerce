using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage?> GetByIdAsync(long id);
    Task<long> AddAsync(ProductImage entity);
    Task UpdateAsync(ProductImage entity);
    Task DeleteAsync(long id);
}
