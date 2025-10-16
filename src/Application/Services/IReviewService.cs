namespace Application.Interfaces.Services;

using Application.Dtos;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(long id);
    Task <long> AddReviewAsync(ReviewDto dto);
    Task UpdateAsync(ReviewDto dto, long id);
    Task DeleteAsync(long id);
}
