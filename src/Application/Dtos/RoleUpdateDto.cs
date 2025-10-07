using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class RoleUpdateDto
{
    public long RoleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

