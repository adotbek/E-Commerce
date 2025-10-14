using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<long> AddAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(long id);
}
