using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappers;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(CategoryMapper.ToDto).ToList();
    }

    public async Task<CategoryGetDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : CategoryMapper.ToDto(entity);
    }

    public async Task<long> AddCategoryAsync(CategoryCreateDto dto)
    {
        var entity = CategoryMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(long id, CategoryUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return;

        CategoryMapper.UpdateEntity(entity, dto);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
