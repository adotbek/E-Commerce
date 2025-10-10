using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ProductImageService(IProductImageRepository repository) : IProductImageService
{
    public async Task<IEnumerable<ProductImageDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken)).Select(p => p.ToDto());

    public async Task<ProductImageDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    public async Task<ProductImageDto> CreateAsync(ProductImageDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await repository.AddAsync(entity, cancellationToken);
        return created.ToDto();
    }

    public async Task<ProductImageDto> UpdateAsync(ProductImageDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var updated = await repository.UpdateAsync(entity, cancellationToken);
        return updated.ToDto();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        => await repository.DeleteAsync(id, cancellationToken);
}
