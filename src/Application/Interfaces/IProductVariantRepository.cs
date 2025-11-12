using Domain.Entities;

namespace Application.Interfaces;

public interface IProductVariantRepository
{
    Task<ICollection<ProductVariant>> GetAllAsync();
    Task<ProductVariant?> GetByIdAsync(long id);
    Task AddAsync(ProductVariant variant);
    Task UpdateAsync(ProductVariant variant);
    Task DeleteAsync(long id);
}
