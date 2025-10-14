using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(long id);
    Task<long> AddAsync(Review entity);
    Task UpdateAsync(Review entity);
    Task DeleteAsync(long id);
}
