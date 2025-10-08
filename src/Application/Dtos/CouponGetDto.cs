using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class CouponGetDto
{
    public long Id { get; set; }
    public string Code { get; set; } = default!;
    public double DiscountPercent { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidUntil { get; set; }
}

