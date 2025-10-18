using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class PaymentOptionUpdateDto
{
    public long Id { get; set; }
    public string CardHolderName { get; set; } = default!;
    public int ExpiryDate { get; set; } = default!;
}