using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class PaymentOptionGetDto
{
    public long Id { get; set; }
    public string CardHolderName { get; set; } = default!;
    public string CardNumberMasked { get; set; } = default!; 
    public string ExpiryDate { get; set; } = default!;
    public string CardType { get; set; } = default!;
}
