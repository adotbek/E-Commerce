namespace Application.Interfaces.Services;

using Application.Dtos;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(long id);
    Task<ReviewDto> CreateAsync(ReviewDto dto);
    Task<ReviewDto> UpdateAsync(ReviewDto dto);
    Task DeleteAsync(long id);
}
