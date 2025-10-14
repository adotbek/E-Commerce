using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<long> AddAsync(Address entity);
    Task<Address?> GetByIdAsync(long id);
    Task<ICollection<Address>> GetByUserIdAsync(long userId);
    Task UpdateAsync(Address entity);
    Task DeleteAsync(long id);
}
