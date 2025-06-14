using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Models.Requests;
public class DepartmentRequest
{
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int? ParentDepartmentId { get; set; }
}
