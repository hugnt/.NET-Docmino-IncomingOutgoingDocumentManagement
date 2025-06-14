using Docmino.Domain.Primitives;

namespace Docmino.Domain.Entities;
public class User : IUserRights, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public int PositionId { get; set; }
    public string WalletAddress { get; set; }
    public string? ImageSignature { get; set; }
    public string? DigitalCertificate { get; set; }

    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }

    public Role Role { get; set; }
    public Position Position { get; set; }
    public IList<UserFeature> UserFeatures { get; set; }
    public IList<UserGroup> UserGroups { get; set; }

    public ICollection<ConfirmProcess> ConfirmProcesses { get; set; }
    public ICollection<ProcessDetail> ProcessDetails { get; set; }
    public ICollection<ProcessHistory> ProcessHistories { get; set; }

}
