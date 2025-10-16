using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(ProductMapper.ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : ProductMapper.ToDto(entity);
    }

    public async Task<long> AddProductAsync(ProductDto dto, long categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

        var entity = ProductMapper.ToEntity(dto);
        entity.CategoryId = categoryId;

        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(ProductDto dto, long categoryId)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);
        if (entity is null)
            throw new KeyNotFoundException($"Product with ID {dto.Id} not found.");

        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");

        ProductMapper.UpdateEntity(entity, dto);
        entity.CategoryId = categoryId;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
