using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Review?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<Review> AddAsync(Review entity, CancellationToken cancellationToken = default);
    Task<Review> UpdateAsync(Review entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
