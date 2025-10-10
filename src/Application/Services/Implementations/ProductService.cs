using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken)).Select(p => p.ToDto());

    public async Task<ProductDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto, long categoryId, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity(categoryId);
        var created = await repository.AddAsync(entity, cancellationToken);
        return created.ToDto();
    }

    public async Task<ProductDto> UpdateAsync(ProductDto dto, long categoryId, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity(categoryId);
        var updated = await repository.UpdateAsync(entity, cancellationToken);
        return updated.ToDto();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
        => await repository.DeleteAsync(id, cancellationToken);
}
