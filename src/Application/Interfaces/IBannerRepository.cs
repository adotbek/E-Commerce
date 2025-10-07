using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBannerRepository
{
    Task<Banner> CreateAsync(Banner entity);
    Task<Banner?> GetByIdAsync(long id);
    Task<ICollection<Banner>> GetAllAsync();
    Task<Banner> UpdateAsync(Banner entity);
    Task<bool> DeleteAsync(long id);
}
