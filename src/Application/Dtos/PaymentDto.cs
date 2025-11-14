using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class PaymentDto
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public PaymentStatus Status { get; set; }

    public long OrderId { get; set; }
    public long UserId { get; set; }
    public long? CardId { get; set; }
}
