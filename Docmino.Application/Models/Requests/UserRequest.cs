using Microsoft.AspNetCore.Http;

namespace Docmino.Application.Models.Requests;

public class UserRequest
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public int PositionId { get; set; }
    public string Username { get; set; }
}
public class AddUserRequest : UserRequest
{
    public string Password { get; set; }
    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }
}

public class UpdateUserRequest : UserRequest
{
    public bool IsDeleted { get; set; }
    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }
}

public class UpdateUserRightRequest
{
    public List<UserRight> UserRights { get; set; }
}

public class UserRight
{
    public Guid Id { get; set; }
    public bool CreateIncomingDocumentRight { get; set; }
    public bool CreateOutgoingDocumentRight { get; set; }
    public bool CreateInternalDocumentRight { get; set; }
    public bool InitialConfirmProcessRight { get; set; }
    public bool ProcessManagerRight { get; set; }
    public bool StoreDocumentRight { get; set; }
    public bool ManageCategories { get; set; }
}


public class UpdatePasswordRequest
{
    public string OldPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}

public class UpdateImageSignatureRequest
{
    public IFormFile? File { get; set; }
}

public class UpdateDigitalCertificateRequest
{
    public IFormFile? File { get; set; }
}

public class UpdateEmailRequest
{
    public string Email { get; set; }
}