using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(long id);
    Task<Review> AddAsync(Review entity);
    Task<Review> UpdateAsync(Review entity);
    Task<bool> DeleteAsync(long id);
}
