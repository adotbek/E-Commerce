using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<Address> CreateAsync(Address entity);
    Task<Address?> GetByIdAsync(long id);
    Task<ICollection<Address>> GetByUserIdAsync(long userId);
    Task<Address> UpdateAsync(Address entity);
    Task<bool> DeleteAsync(long id);
}
