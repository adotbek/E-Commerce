using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<Product> AddAsync(Product entity);
    Task<Product> UpdateAsync(Product entity);
    Task<bool> DeleteAsync(long id);
}
