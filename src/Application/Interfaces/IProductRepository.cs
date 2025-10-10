using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product entity, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
