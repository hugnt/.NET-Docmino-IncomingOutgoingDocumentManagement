using Docmino.Domain.Enums;
using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class Document : Entity, IIncomingDocument, IOutgoingDocument, IInternalDocument, IAuditableEntity
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Guid DocumentRegisterId { get; set; }
    public int FieldId { get; set; }
    public string? CodeNumber { get; set; }
    public string? CodeNotation { get; set; }

    // VB đên - Ngày ký | VB đi - ngày văn bản
    public DateOnly IssuedDate { get; set; }

    //VB đến - Cơ quan ban hành | VB đi - Đơn vị soạn
    public int? OrganizationId { get; set; }

    //VB nội bộ
    public int? DepartmentId { get; set; }

    public string? Subject { get; set; }
    public int PageAmount { get; set; }
    public string? Description { get; set; }
    public SecurePriority SecurePriority { get; set; }
    public UrgentPriority UrgentPriority { get; set; }

    public string? ArrivalNumber { get; set; }
    public DateOnly? ArrivalDate { get; set; }
    public string? ToPlaces { get; set; }
    public DateOnly? DueDate { get; set; }
    public int IssuedAmount { get; set; }

    public Guid? StorageId { get; set; }
    public DocumentStatus DocumentStatus { get; set; } = DocumentStatus.Draff;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public DocumentRegister DocumentRegister { get; set; }
    public DocumentCategory Category { get; set; }
    public DocumentField Field { get; set; }
    public Organization? Organization { get; set; }
    public Department? Department { get; set; }
    public Storage? Storage { get; set; }
    public ConfirmProcess Process { get; set; }
    public ICollection<DocumentFile> DocumentFiles { get; set; }

}
