using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class OrderDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public long UserId { get; set; }
    public long? AddressId { get; set; }

    public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
