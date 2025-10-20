using Application.Dtos;
using Application.DTOs;

namespace Application.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto?> GetByIdAsync(long id);
    //
    Task<long> AddCategoryAsync(CategoryCreateDto dto);
    //
    Task UpdateAsync(long id, CategoryUpdateDto dto);
    //
    Task DeleteAsync(long id);
}
