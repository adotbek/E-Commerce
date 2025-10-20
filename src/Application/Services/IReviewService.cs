namespace Application.Interfaces.Services;

using Application.Dtos;

public interface IReviewService
{
    //
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(long id);
    Task <long> AddReviewAsync(ReviewDto dto);
    Task UpdateAsync(ReviewDto dto, long id);
    Task DeleteAsync(long id);
    Task<IEnumerable<ReviewDto>> GetByProductIdAsync(long productId);    
    Task<IEnumerable<ReviewDto>> GetByUserIdAsync(long userId);           
    Task<double> GetAverageRatingByProductIdAsync(long productId);        
    Task<int> GetReviewCountByProductIdAsync(long productId);             
    Task<bool> ExistsAsync(long userId, long productId);          
    //
    Task<IEnumerable<ReviewDto>> GetRecentReviewsAsync(int count = 10);   
}
