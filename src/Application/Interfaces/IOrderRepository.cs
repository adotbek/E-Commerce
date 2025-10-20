﻿using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(long id);
    Task<long> AddAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(long id);
    Task<IEnumerable<Order>> GetByUserIdAsync(long userId);
    Task UpdateStatusAsync(long id, string status);
    Task<IEnumerable<Order>> GetByStatusAsync(string status);
    Task<decimal> CalculateTotalAmountAsync(long orderId);
    Task<IEnumerable<Order>> GetRecentOrdersAsync(int count);
    Task<bool> ExistsAsync(long orderId);
    Task<IEnumerable<Order>> GetPendingOrdersAsync();
    Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to);
    Task<int> GetTotalOrdersCountAsync();

}
