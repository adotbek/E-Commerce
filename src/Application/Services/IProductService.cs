using Application.Dtos;
using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(long id);
    Task<long> AddProductAsync(ProductCreateDto dto, long categoryId);
    Task UpdateAsync(ProductDto dto, long categoryId, long id);
    Task DeleteAsync(long id);
    Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(long categoryId);
    Task<IEnumerable<ProductDto>> GetFeaturedAsync();          
    Task<IEnumerable<ProductDto>> GetNewArrivalsAsync();       
    Task<IEnumerable<ProductDto>> SearchAsync(string keyword); 
    Task<bool> ExistsAsync(long id);                           
    Task UpdateStockAsync(long id, int quantity);              
    Task<IEnumerable<ProductDto>> GetOutOfStockAsync();         
    Task<decimal?> GetDiscountPriceAsync(long productId);       
}


