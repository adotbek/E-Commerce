using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(long id);
    Task<long> AddAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(long categoryId);
    Task<IEnumerable<Product>> GetFeaturedAsync();
    Task<IEnumerable<Product>> GetNewArrivalsAsync();
    Task<IEnumerable<Product>> SearchAsync(string keyword);
    Task<bool> ExistsAsync(long id);
    Task UpdateStockAsync(long id, int quantity);
    Task<IEnumerable<Product>> GetOutOfStockAsync();
    Task<decimal?> GetDiscountPriceAsync(long productId);
}
