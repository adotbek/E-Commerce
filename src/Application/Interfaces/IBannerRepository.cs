using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IBannerRepository
{
    Task<long> AddAsync(Banner entity);
    Task<Banner?> GetByIdAsync(long id);
    Task<ICollection<Banner>> GetAllAsync();
    Task UpdateAsync(Banner entity);
    Task DeleteAsync(long id);
}
