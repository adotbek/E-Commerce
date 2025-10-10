using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductImage?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<ProductImage> AddAsync(ProductImage entity, CancellationToken cancellationToken = default);
    Task<ProductImage> UpdateAsync(ProductImage entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
