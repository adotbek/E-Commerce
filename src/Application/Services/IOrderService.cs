using Application.Dtos;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> PlaceOrderAsync(long userId, OrderCreateDto dto);
    Task<OrderDto?> GetByIdAsync(long userId, long orderId);
    Task<ICollection<OrderDto>> GetUserOrdersAsync(long userId);
    Task CancelOrderAsync(long userId, long orderId);
}
