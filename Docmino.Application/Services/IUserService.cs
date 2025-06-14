using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IUserService
{
    public Task<Result> LookupApprover();
    public Task<Result> GetAll(FilterRequest filter);
    public Task<Result> GetById(Guid id);
    public Task<Result> Add(AddUserRequest modelRequest);
    public Task<Result> Update(Guid id, UpdateUserRequest modelRequest);
    public Task<Result> Delete(Guid id);
    public Task<Result> UpdateRights(UpdateUserRightRequest rightRequest);

    public Task<Result> UpdateImageSignature(UpdateImageSignatureRequest imageSignatureRequest);
    public Task<Result> UpdateDigitalCertificate(UpdateDigitalCertificateRequest digitalCertificate);
    public Task<Result> UpdateEmail(UpdateEmailRequest emailRequest);
    public Task<Result> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);


}
