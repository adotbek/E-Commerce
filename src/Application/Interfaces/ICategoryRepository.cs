using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(long id);
    Task AddAsync(Category entity);
    Task UpdateAsync(Category entity);
    Task DeleteAsync(long id);
}
