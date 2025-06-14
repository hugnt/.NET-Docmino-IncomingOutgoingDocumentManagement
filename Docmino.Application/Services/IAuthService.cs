using Docmino.Application.Models;
using Docmino.Application.Models.Requests;

namespace Docmino.Application.Services;
public interface IAuthService
{
    public Task<Result> Login(LoginRequest loginRequest);
    public Task<Result> Logout(LogoutRequest logoutRequest);
    public Task<Result> ExtendSession(ExtendSessionRequest extendSessionRequest);
    public Task<Result> GetCurrentUserContext();
}
