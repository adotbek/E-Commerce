using Application.Dtos;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ReviewService(IReviewRepository repository) : IReviewService
{
    public async Task<IEnumerable<ReviewDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken)).Select(r => r.ToDto());

    public async Task<ReviewDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    public async Task<ReviewDto> CreateAsync(ReviewDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await repository.AddAsync(entity, cancellationToken);
        return created.ToDto();
    }

    public async Task<ReviewDto> UpdateAsync(ReviewDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var updated = await repository.UpdateAsync(entity, cancellationToken);
        return updated.ToDto();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        => await repository.DeleteAsync(id, cancellationToken);
}
