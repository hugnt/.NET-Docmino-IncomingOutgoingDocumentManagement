using Docmino.Domain.Entities;

namespace Docmino.Domain.Primitives;
public interface IInternalDocument
{
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }

}
