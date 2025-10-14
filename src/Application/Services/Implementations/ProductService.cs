using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(ProductMapper.ToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : ProductMapper.ToDto(entity);
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto, long categoryId)
    {
        var entity = ProductMapper.ToEntity(dto, categoryId);
        var created = await _repository.AddAsync(entity);
        return ProductMapper.ToDto(created);
    }

    public async Task<ProductDto> UpdateAsync(ProductDto dto, long categoryId)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Product with Id={dto.Id} not found.");

        ProductMapper.UpdateEntity(existing, dto, categoryId);

        var updated = await _repository.UpdateAsync(existing);
        return ProductMapper.ToDto(updated);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            throw new KeyNotFoundException($"Product with Id={id} not found.");
        return true;
    }
}
