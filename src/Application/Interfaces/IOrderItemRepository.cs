using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IOrderItemRepository
{
    Task<ICollection<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(long id);
    Task AddAsync(OrderItem item);
    Task UpdateAsync(OrderItem item);
    Task DeleteAsync(long id);
    Task AddRangeAsync(ICollection<OrderItem> items);
}