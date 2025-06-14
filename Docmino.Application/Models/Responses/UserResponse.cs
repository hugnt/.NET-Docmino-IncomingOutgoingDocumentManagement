namespace Docmino.Application.Models.Responses;
public class UserResponse
{
    public Guid Id { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }

    public string? PositionName { get; set; }
    public string? DepartmentName { get; set; }

    public string? DigitalCertificate { get; set; }
    public string WalletAddress { get; set; }
    public string? ImageSignature { get; set; }

    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }

    public List<string>? Groups { get; set; }
}

public class UserDetailResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public int PositionId { get; set; }
    public string? PositionName { get; set; }
    public string? DepartmentName { get; set; }

    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }
    public bool IsDeleted { get; set; }

    public List<string>? Groups { get; set; }


}
