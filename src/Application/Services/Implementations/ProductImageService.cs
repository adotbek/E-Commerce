using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _repository;

    public ProductImageService(IProductImageRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductImageDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(ProductImageMapper.ToDto);
    }

    public async Task<ProductImageDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : ProductImageMapper.ToDto(entity);
    }

    public async Task<ProductImageDto> CreateAsync(ProductImageDto dto)
    {
        var entity = ProductImageMapper.ToEntity(dto);
        var created = await _repository.AddAsync(entity);
        return ProductImageMapper.ToDto(created);
    }

    public async Task<ProductImageDto> UpdateAsync(ProductImageDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing is null)
            throw new KeyNotFoundException($"ProductImage with Id={dto.Id} not found.");

        ProductImageMapper.UpdateEntity(existing, dto);

        var updated = await _repository.UpdateAsync(existing);
        return ProductImageMapper.ToDto(updated);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new KeyNotFoundException($"ProductImage with Id={id} not found.");

        await _repository.DeleteAsync(id);
    }
}
