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
        var images = await _repository.GetAllAsync();
        return images.Select(ProductImageMapper.ToDto).ToList();
    }

    public async Task<ProductImageDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : ProductImageMapper.ToDto(entity);
    }

    public async Task<long> AddProductImageAsync(ProductImageDto dto)
    {
        var entity = ProductImageMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(ProductImageDto dto, long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Product image with ID {id} not found.");

        ProductImageMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
