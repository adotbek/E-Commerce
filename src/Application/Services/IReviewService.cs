using Application.Dtos;
using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReviewDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<ReviewDto> CreateAsync(ReviewDto dto, CancellationToken cancellationToken = default);
    Task<ReviewDto> UpdateAsync(ReviewDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
