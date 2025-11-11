using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class PaymentCreateDto
{
    public long OrderId { get; set; }
    public decimal Amount { get; set; }
    //public PaymentMethod Method { get; set; } = default!;
    public string TransactionId { get; set; } = default!;
    public long? PaymentOptionId { get; set; }
    public decimal? Discount { get; set; } = null;

}
