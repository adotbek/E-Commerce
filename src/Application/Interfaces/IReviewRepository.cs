using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(long id);
    Task<long> AddAsync(Review entity);
    Task UpdateAsync(Review entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<Review>> GetByProductIdAsync(long productId);
    Task<IEnumerable<Review>> GetByUserIdAsync(long userId);
    Task<double> GetAverageRatingByProductIdAsync(long productId);
    Task<int> GetReviewCountByProductIdAsync(long productId);
    Task<bool> ExistsAsync(long userId, long productId);
    Task<IEnumerable<Review>> GetRecentReviewsAsync(int count = 10);
}
