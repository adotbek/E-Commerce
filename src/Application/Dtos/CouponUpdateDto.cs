using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class CouponUpdateDto
{
    public double DiscountPercent { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidUntil { get; set; }
}