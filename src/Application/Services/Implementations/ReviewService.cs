using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _repository.GetAllAsync();
        return reviews.Select(r => r.ToDto());
    }

    public async Task<ReviewDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity?.ToDto();
    }

    public async Task<long> AddReviewAsync(ReviewDto dto)
    {
        var entity = dto.ToEntity();
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(ReviewDto dto, long id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"Review with ID {id} not found.");

        existing.UpdateEntity(dto);
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
