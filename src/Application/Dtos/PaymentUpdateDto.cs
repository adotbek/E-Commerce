using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class PaymentUpdateDto
{
    public long Id { get; set; }
    public string Status { get; set; } = default!;
}
